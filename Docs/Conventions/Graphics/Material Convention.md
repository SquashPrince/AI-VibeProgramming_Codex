# Material 컨벤션

## 기본 개념과 역할

Material은 Shader와 색상·Texture 등의 설정을 담는 에셋이다. 여러 대상이 공유할 수 있으므로 설정 변경 전에 사용처와 영향 범위를 확인한다.
작업 진입은 [그래픽 공통](<Common Convention.md>), Shader 선택은 [Shader](<Shader Convention.md>)을 따른다.

## 사용자 생성·설정

- Material 에셋은 에이전트가 직접 만들지 않고 사용자가 생성한다. 에이전트는 이름·경로·Shader 선택·프로퍼티·Texture 연결·할당 방법을 안내한다.
- 생성 전에 기존 Material과 사용처를 확인하고 재사용·별도 생성의 이유를 설명한다.
- 같은 설정을 함께 쓸 대상은 재사용, 외형을 독립 관리할 대상은 별도 Material을 검토한다. 오브젝트마다 무조건 복제하거나 비슷하다는 이유로 공유를 강제하지 않는다.
- 공유 Material 변경과 Shader 교체는 다른 사용처·프로퍼티·Texture 연결에 미치는 영향을 먼저 보고한다.
- 런타임 코드가 생성하는 Material 인스턴스는 사용자 생성 에셋과 구분한다. 필요한 이유·생성 시점·수명·해제 책임을 브리핑하고 승인받은 범위에서 구현한다.

## 공유·개별·런타임 변경

| 목적 | 검토 방식 | 확인할 영향 |
|---|---|---|
| 공유 대상 모두의 외형 변경 | 공유 Material 설정 변경 | 모든 사용처의 외형 |
| 특정 외형을 독립 에셋으로 관리 | 사용자 별도 Material 생성·할당 | 에셋 수·공유 관계 |
| 실행 중 개별 대상만 변경 | 런타임 인스턴스 또는 MaterialPropertyBlock 검토 | 배칭·메모리·초기화·해제 |

### Material 인스턴스와 MaterialPropertyBlock

| 항목 | Material 인스턴스 | MaterialPropertyBlock |
|---|---|---|
| 원리 | 독립적인 Material 복사본 사용 | Material을 유지하고 Renderer에 프로퍼티 덮어쓰기 |
| 표현 | Material 프로퍼티·키워드·렌더링 설정 등을 개별 변경 | 색·숫자·Texture 등 프로퍼티 변경, 렌더링 상태 변경은 지원하지 않음 |
| 관리 | 복사본 캐싱·재사용·해제 책임 | 덮어쓰기 초기화·다른 코드와의 충돌 관리 |
| 비용 검토 | 인스턴스 수·메모리·배칭 조건 | 대상 렌더링 경로·SRP Batcher 영향 |

예를 들어 같은 Material을 쓰는 몬스터 중 하나만 피격 색으로 바꿀 때, 복사본을 주거나 해당 Renderer에만 값을 덮어쓰는 방법을 비교한다.
`Renderer.material` 접근으로 복사본이 생성될 수 있으므로 생성 여부를 확인하고 반복 접근·불필요한 생성·정리 누락을 피한다.
MaterialPropertyBlock은 URP의 SRP Batcher 호환성을 잃게 하므로 무조건 저렴한 기본값으로 선택하지 않는다.
각 동작의 근거는 [Renderer.material](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Renderer-material.html)과 [MaterialPropertyBlock](https://docs.unity3d.com/6000.3/Documentation/ScriptReference/MaterialPropertyBlock.html)을 참조한다.

## 렌더링 최적화 설명

### SRP Batcher

SRP Batcher는 호환되는 Shader Variant의 그리기 준비에 드는 CPU 비용을 줄이는 기능이다. 다른 Material을 쓴다고 항상 제외되거나 모든 그리기가 하나의 드로우콜이 되는 것은 아니다.

- 현재 URP·Shader의 호환 조건과 실제 렌더링 경로를 확인한다.
- 프로퍼티·상수 버퍼 구성과 MaterialPropertyBlock 등의 영향을 설명한다.
- 검색어: `Unity URP SRP Batcher compatibility`, `SRP Batcher MaterialPropertyBlock`.
- 사용자 테스트: 같은 장면에서 Frame Debugger로 사용 경로·배치가 나뉘는 이유를 확인하고 CPU 렌더링 시간을 비교한다. 드로우콜 수만으로 효과를 판단하지 않는다.

### GPU Instancing

GPU Instancing은 같은 Mesh·Material을 사용하는 여러 대상을 묶어 그리는 방식이다. 실제 적용에는 Shader·Renderer·플랫폼·렌더링 경로 등의 조건이 있다.

- 대상의 수와 반복 구성, 인스턴스별 변경값, 현재 배칭 경로를 확인해 제안한다.
- 체크박스 활성화만으로 적용이나 속도 향상을 보장하지 않는다. SRP Batcher와의 우선순위·호환성 및 다른 렌더링 방식의 영향을 현재 버전에서 확인한다.
- 검색어: `Unity GPU Instancing requirements`, `URP GPU Instancing SRP Batcher`.
- 사용자 테스트: 같은 대상 수·카메라에서 실제 인스턴싱 여부를 Frame Debugger로 확인하고 CPU·GPU 시간과 렌더링 통계를 비교한다. 한쪽 비용만 줄었는지 구분한다.

### 적용 판단

- 인스턴스·PropertyBlock·공유 Material을 어느 하나로 고정하지 않는다. 개별 변화, 수명, 메모리와 배칭 조건을 비교한다.
- 최적화 검색 키워드를 제공해 사용자가 찾은 대안을 함께 검토한다.
- 공통 제안·가정·사용자 변경·측정 절차는 [성능 컨벤션](<../Safety/Performance Convention.md>)을 따른다. 승인 없이 프로젝트 전체 배칭 설정을 변경하지 않는다.

## 사용자 적용 후 확인

- 생성·설정 단계별로 대상 Material, Shader, 입력값과 할당 위치를 안내한다.
- 실제 대상에서 색·Texture·투명도·조명 반응 등 요구한 결과를 확인하게 한다.
- 개별 변경은 다른 대상에 번지지 않는지, 효과 종료·비활성화·재사용 시 원상 복구되는지 확인한다.
- 런타임 인스턴스는 생성·재사용·해제 동작과 메모리를 확인한다.
- 제안 전에는 예상 효과만 보고하고, 사용자 결과를 확인한 후 실제 결과로 구분해 기록한다.

## 공식 참고

- [GPU Instancing](https://docs.unity3d.com/6000.3/Documentation/Manual/GPUInstancing.html)
- [URP SRP Batcher](https://docs.unity3d.com/6000.3/Documentation/Manual/SRPBatcher.html)
