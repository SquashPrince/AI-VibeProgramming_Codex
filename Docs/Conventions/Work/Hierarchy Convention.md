# Scene·Prefab 계층구조 컨벤션

## 목적과 적용 범위

- Scene과 Prefab의 역할을 쉽게 파악하고 필요한 기능과 참조를 명확하게 구성한다.
- 공통 명명 기준은 Scene 오브젝트, Prefab 내부 요소, UI 요소와 폴더 계층에 적용한다.
- 아래 이름과 계층도는 구성 방식을 설명하는 예시이며 필수 생성 목록이 아니다. Player·Enemy·무기는 적용 대상의 예시이고, 다른 대상도 실제 역할과 요구에 따라 구성한다.
- 폴더 배치와 파일 생성·이동 절차는 [폴더·파일 구조 컨벤션](<Structure Convention.md>)을 따른다.

## 공통 계층 명명

- 기본 명명 언어는 영어로 한다. 부모와 자식은 같은 언어로 일관되게 명명한다. 다른 언어를 선택한 경우에도 같은 기준을 적용한다.
- 부모 경로로 대상이 명확하면 자식 이름에는 담당 역할만 적는다. 부모의 대상명·역할명·번호를 불필요하게 반복하지 않는다.
- 예를 들어 `ActiveCharacterArea/ActiveCharacterPortrait`는 `ActiveCharacterArea/Portrait`, `PartyArea/PartyMember2/PartyPortrait2`는 `PartyArea/Member2/Portrait`로 제안한다.
- 폴더에서도 같은 기준을 적용한다. 예를 들어 `Prefabs/Player/PlayerWeapons/`보다 `Prefabs/Player/Weapons/`처럼 문맥을 활용한다. 대표 타입과 파일명의 일치 등 코드 식별자 규칙은 [코드 컨벤션](<../Code/Code Convention.md>)을 유지한다.
- 서로 다른 부모 아래의 동일 이름은 전체 경로로 구분할 수 있다. 같은 부모 아래에서 이름이 충돌하거나 참조 대상이 모호해지는지 확인한다.
- 기존 이름은 이 기준만을 이유로 임의 변경하지 않는다. 이름 변경에 따른 참조·검색 경로의 영향과 변경 대상을 보고하고 승인받는다.

## 기존 구조 활용

- 제안 전에 기존 오브젝트·Prefab·컴포넌트·참조 관계와 관련 에셋을 확인한다.
- 적절한 기존 구조를 활용하고, 정리만을 위해 불필요한 부모나 자식을 만들지 않는다.
- 추가 세분화가 필요하면 이유와 제안 구조를 사용자에게 보고하고 답변을 받은 뒤 진행한다.

## Scene 구성

- 같은 역할의 오브젝트가 많아 탐색·선택·관리가 불편한 영역만 부모로 묶는 방식을 검토한다. 개수만으로 묶음을 강제하지 않는다.
- 모든 최상위 오브젝트를 일괄적으로 정리용 부모 아래에 넣지 않는다.
- 정리용 부모의 Transform은 Position `(0, 0, 0)`, Rotation `(0, 0, 0)`, Scale `(1, 1, 1)`을 기본으로 한다.
- 부모 생성·변경 전 Transform과 활성화 상태가 자식에게 미치는 영향을 확인한다.

```text
Scene
├─ Player
├─ MainCamera
├─ DirectionalLight
├─ Environment
│  ├─ Ground
│  ├─ Ramp
│  └─ Platforms
│     ├─ Platform1
│     └─ Platform2
├─ Enemies
│  ├─ Enemy1
│  └─ Enemy2
├─ HUDCanvas
└─ EventSystem
```

## 개별 오브젝트·Prefab 구성

- 외형과 기능을 하나의 단위로 관리할 대상은 빈 GameObject를 루트로 만들고 외형과 필요한 기능을 자식으로 구성한다.
- 빈 루트는 외형과 분리한 기준 오브젝트를 뜻한다. 필요한 스크립트·물리·충돌 컴포넌트를 가질 수 있으며 Transform만 허용한다는 뜻이 아니다.
- 제어 스크립트는 기본적으로 루트에 배치한다. 자식에서 처리할 책임이 있으면 이유와 배치 방식을 제안한다.
- 필요한 자식 오브젝트·컴포넌트는 Inspector 직접 참조를 기본으로 한다. 상세 기준은 [참조 관리 컨벤션](<../Code/Reference Convention.md>)을 따른다.
- Collider는 용도에 따라 배치한다. 모두 루트에 모으거나 모두 자식으로 분리하지 않는다. 충돌과 감지를 분리하는 편이 명확하면 루트에 충돌용 Collider, 자식에 감지용 Collider를 배치하는 등의 안을 제안한다.
- Prefab을 배치할 장착 위치와 해당 Prefab의 내부 루트는 구분한다.

```text
Player                         ← 제어 스크립트와 필요한 충돌 컴포넌트
├─ Visual                      ← 캐릭터 외형
├─ Weapon                      ← 장착 위치
│  └─ Gun                      ← 별도 총 Prefab의 인스턴스
│     ├─ Visual                ← 총 외형
│     ├─ Muzzle                ← 발사 위치·방향 기준
│     └─ EffectPoint           ← 필요한 이펙트 발생 기준점
├─ CameraPivot                 ← 필요한 경우에만 추가
└─ DetectionArea               ← 별도 감지가 필요한 경우에만 추가
```

## 카메라

- 기본적으로 플레이어 계층 밖에서 독립 제어하고, 필요한 경우 내부 Pivot을 참조하여 추적하는 방식을 검토한다.
- 플레이어의 이동·회전·크기·활성화 상태가 카메라 제어를 의도치 않게 제한하지 않도록 한다. 구체적인 배치와 추적 방식은 프로젝트 요구에 따라 정한다.
- 카메라 관련 구성이 요청 범위에 필요하지 않으면 계층 제안에 미리 포함하지 않는다.

## 풀링 대상의 계층과 반환

- 대상마다 반환 부모를 명확하게 지정한다. 사용 중에는 `SetParent(null)` 등으로 부모를 해제할 수 있으며 별도 관리 부모를 강제하지 않는다.
- 사용 종료 후 비활성 상태로 보관할 때는 지정된 부모 아래로 복귀해야 한다. 반환된 대상을 Hierarchy 최상위 등에 남겨두지 않는다.
- 반환 부모 복원과 함께 다음 사용에 필요한 상태를 초기화한다. 총알 예시에서는 Muzzle로 반환하고 `localPosition = Vector3.zero`로 초기화한다.
- 회전·크기·속도 등의 추가 초기화는 실제 동작에 맞게 제안한다. 모든 대상의 반환 부모를 Muzzle로 강제하지 않는다.

```text
대기: Gun/Muzzle/Bullet         ← 비활성
발사: Bullet                   ← Muzzle 위치·방향으로 발사, 부모 해제 허용
반환: Gun/Muzzle/Bullet         ← 부모 복원, 로컬 위치 초기화, 비활성
```

총과 총알은 예시이며, 이펙트 등 다른 풀링 대상에도 지정된 반환 부모와 필요한 초기화를 정한다.

## UI 구성

### 공통

- 화면의 실제 역할과 관리 필요에 따라 요소를 묶는다. 배경·상태·조작·알림 등은 예시이며 필수 영역이 아니다.
- UI의 자식 요소는 실제 역할과 요구사항에 따라 구성한다. 문서의 예시 이름과 구조를 필수 구성으로 취급하지 않는다. 특정 프로젝트나 Prefab의 사용을 전제로 하지 않는다.
- UI도 필요에 따라 빈 루트에 제어 스크립트를 두고 하위 요소를 참조할 수 있다. 구체적인 연결 방식은 uGUI·UI Toolkit의 특성에 맞게 정한다.

### uGUI

- Canvas 아래 GameObject·RectTransform 계층으로 구성한다. 적절한 기존 Canvas와 부모를 활용한다.
- Canvas 분리의 판단과 비용은 아래 `UI 갱신 특성과 계층 분리`를 따른다.
- 구조 변경 시 RectTransform·앵커·레이아웃·겹침 순서·입력 영향을 확인한다.

```text
ActionBattleHUD                 ← 기존 루트 Canvas
├─ ScreenFrame
├─ ActiveCharacterArea
│  ├─ Portrait
│  ├─ HP
│  └─ SkillEnergy
├─ PartyArea
│  ├─ Member1
│  │  ├─ Portrait
│  │  ├─ HP
│  │  └─ SkillEnergy
│  └─ Member2
│     ├─ Portrait
│     ├─ HP
│     └─ SkillEnergy
├─ EnemyArea
│  ├─ Portrait
│  ├─ HP
│  └─ StaggerGauge
└─ ActionArea
   ├─ AttackIcon
   ├─ SkillIcon
   ├─ DodgeIcon
   ├─ SwitchIcon
   └─ UltimateArea
      ├─ Icon
      └─ Gauge
```

위 구조는 역할별 구성 예시이며, 자식 Canvas의 추가 위치는 실제 갱신 방식과 검증 결과에 따라 별도로 정한다.

### UI Toolkit

- Scene의 UIDocument·제어 스크립트 구성과 UXML 또는 코드로 구성하는 Visual Tree를 구분하여 제안한다. 개별 VisualElement를 GameObject 자식으로 취급하지 않는다.
- UIDocument는 Inspector에서 참조하고, 내부 요소는 Visual Tree 조회 등 UI Toolkit에 맞는 방식으로 연결한다.
- 문서·패널 분리와 갱신 비용의 판단은 아래 `UI 갱신 특성과 계층 분리`를 따른다.

#### 기본 개념과 설명 방식

| 용어 | 역할 | uGUI와 비교할 때 주의할 점 |
|---|---|---|
| UIDocument | Scene에서 UI 문서를 연결하고 표시하는 컴포넌트 | Canvas와 대응해 이해할 수 있지만 같은 갱신 구조는 아니다. |
| UXML | UI 요소의 종류와 부모·자식 구조를 정의하는 파일 | GameObject 계층을 저장한 Prefab과 동일한 형식은 아니다. |
| USS | 색상·크기·배치 등 UI 스타일을 정의하는 파일 | UI 컴포넌트의 Inspector 설정과 일대일 대응하지 않는다. |
| VisualElement | UI 트리를 구성하는 기본 요소 | 개별 GameObject나 MonoBehaviour가 아니다. |
| Visual Tree | VisualElement들의 부모·자식 구조 | Scene Hierarchy와 구분한다. |
| PanelSettings | UI 패널의 표시·스케일링·렌더링 관련 설정 에셋 | 여러 UIDocument가 공유할 수 있다. |

- UI Toolkit을 처음 제안·적용할 때는 필요한 개념과 uGUI와의 차이를 먼저 설명한다. 이미 설명한 개념은 후속 작업에서 불필요하게 반복하지 않는다.
- 설명의 상세도는 사용자 이해도에 맞추고, 확실성 표시는 공식 근거와 실제 검증 상태에 맞춘다. 사용자가 익숙하지 않다는 이유로 확인된 기본 개념을 불확실한 내용으로 취급하지 않는다.
- 확인된 동작, 프로젝트 요구에 따른 구조 제안, 측정 전 성능 효과를 구분한다. 버전별 동작이 불명확하면 사용 중인 Unity 버전의 공식 문서를 확인하고 미확인 사항을 표시한다.

```text
Scene: HUD                     ← UIDocument, 제어 스크립트

Visual Tree:
HUDRoot
├─ StatusArea
│  ├─ HealthBar
│  └─ ScoreLabel
└─ ActionArea
   ├─ JumpButton
   └─ PauseButton
```

## UI 갱신 특성과 계층 분리

### 목적과 적용 기준

- UI 계층은 역할과 관리 편의를 기준으로 구성하되, 변경에 따른 갱신 비용도 함께 고려한다.
- uGUI와 UI Toolkit은 갱신·렌더링 구조가 다르므로 동일한 분리 방식을 일괄 적용하지 않는다.
- 에이전트는 실제 UI 구성과 변경 방식을 확인한 뒤 분리가 필요한 이유, 제안 구조, 예상 영향과 검증 방법을 설명한다.
- 성능 개선 효과는 실제 측정으로 확인한다. 측정 전의 예상 효과를 검증된 결과로 보고하지 않는다.

### uGUI의 Canvas 분리

#### 갱신 특성

- UI의 텍스트·이미지·게이지·Transform 등이 변경되면 변경 내용에 따라 그래픽·레이아웃 갱신이나 Canvas 배치 정보 재구성 작업이 발생할 수 있다.
- 모든 변경이 모든 자식의 모든 정보를 다시 만든다는 의미는 아니다. 변경 종류와 실제 갱신 범위를 구분한다.
- 자주 변경되는 요소와 거의 변경되지 않는 요소가 같은 Canvas에 있으면, 배치 정보 재구성 시 변경되지 않은 요소까지 처리 범위에 포함되어 비용이 커질 수 있다.
- 화면 위치가 고정되어 있어도 텍스트·이미지·게이지 등이 변경되면 동적 요소로 판단한다.
- 이 문서에서 정적 UI는 자주 변경되지 않는 UI를 뜻하며, GameObject의 Static 설정을 뜻하지 않는다.

#### 분리 판단

- 다음 사항을 함께 확인하여 별도 Canvas 또는 자식 Canvas 분리를 검토한다.
  - 요소가 변경되는 빈도
  - 함께 변경되는 요소의 범위
  - 한 Canvas에 포함된 요소의 규모
  - 표시·숨김과 애니메이션의 적용 범위
  - 렌더링 순서와 입력 처리
- 자주 함께 갱신되는 요소는 같은 Canvas에 묶는 방식을 검토한다.
- 갱신 시점이나 빈도가 크게 다른 영역은 분리를 검토한다.
- 모든 UI를 정적·동적 두 Canvas로 나누거나, 모든 역할별 부모에 Canvas를 추가하지 않는다.
- 작은 UI에서 갱신 비용이 문제되지 않는다면 단일 Canvas를 유지할 수 있다.

#### 자식 Canvas 활용

- 기존 화면 구조를 유지하면서 일부 영역의 배치 갱신을 분리할 필요가 있으면 자식 Canvas를 검토한다.
- 빈 GameObject나 RectTransform 부모만 추가하는 것은 Canvas 분리가 아니다.
- 자식 Canvas는 자체 배치 처리를 하지만, 부모의 레이아웃·크기·Transform 변경 등에 따른 영향까지 모두 차단하는 것은 아니다.
- Canvas 분리 시 기존 RectTransform·앵커·레이아웃·겹침 순서와 입력 동작이 유지되는지 확인한다.

#### 구성 예시

```text
HUDCanvas                      ← 고정 배경·장식
├─ Background
├─ Frame
└─ StatusArea                  ← 자식 Canvas
   ├─ HealthBar
   ├─ ScoreLabel
   └─ TimerLabel
```

- 위 구조는 예시이며 필수 구성이나 이름이 아니다.
- StatusArea의 요소가 자주 갱신되고 배경·장식은 거의 변경되지 않는 상황에서 검토할 수 있다.
- StatusArea 내부에서도 변경 빈도가 크게 다르면 추가 분리를 검토할 수 있으나, 그 필요와 비용을 먼저 설명한다.

#### 분리의 비용

- 서로 다른 Canvas 사이에서는 UI 배칭이 분리되므로, Canvas를 늘리면 드로우콜과 관리 비용이 증가할 수 있다.
- 갱신 범위를 줄이는 효과와 렌더링 비용 증가를 함께 판단한다.
- 성능 문제의 원인이 과도한 레이아웃 갱신, 투명 UI의 중첩, 불필요한 입력 검사 등이라면 Canvas 분리만으로 해결된다고 판단하지 않는다.

### UI Toolkit의 구성과 갱신

#### 구조 구분

- Scene의 UIDocument 오브젝트 구성과 문서 내부 Visual Tree를 구분한다.
- UIDocument를 나누는 것과 렌더링 패널을 나누는 것은 다르다.
- 여러 UIDocument가 같은 PanelSettings를 공유할 수 있으므로, 문서 개수만으로 렌더링과 갱신이 독립된다고 판단하지 않는다.

#### 분리 판단

- 화면의 역할, 재사용 범위, 표시·숨김 시점과 제어 책임을 기준으로 문서와 요소를 구성한다.
- 정적·동적 요소라는 이유만으로 UIDocument나 PanelSettings를 분리하지 않는다.
- Visual Tree에서 부모 요소를 분리하는 것은 구조 관리에 도움이 될 수 있지만, uGUI의 Canvas 분리와 같은 갱신 격리 효과를 보장하지 않는다.
- 별도 문서나 패널이 필요하면 기능상 이유와 성능·자원 비용을 함께 설명한다.

#### 갱신 비용 검토

- 변경 내용에 따라 스타일 재계산, 레이아웃 계산, 지오메트리 갱신과 렌더링 등에 미치는 영향을 확인한다.
- 값이 바뀌지 않았는데 같은 표시 값을 반복 설정하는 등 불필요한 갱신 요청을 줄인다.
- 이동 애니메이션은 레이아웃 변경이 필요한지 확인한다. 단순한 시각적 이동이면 translate 등 레이아웃 재계산을 피할 수 있는 방식을 검토한다.
- UsageHints 등 세부 최적화는 요소의 실제 변경 방식과 사용 중인 Unity 버전을 확인하여 필요한 경우에만 제안한다.
- 계층을 나누는 것만으로 성능 문제가 해결된다고 판단하지 않는다.

#### 구성 예시

```text
HUDRoot
├─ BackgroundArea
├─ StatusArea
│  ├─ HealthBar
│  └─ ScoreLabel
└─ NotificationArea
```

- 위 구조는 역할별 Visual Tree 구성 예시이며, 각 영역을 별도 UIDocument나 패널로 만들라는 의미가 아니다.
- 성능상 분리가 필요한지는 실제 변경 방식과 측정 결과로 판단한다.

### 검증과 보고

- 구조 변경 전후에 동일한 화면 상태와 갱신 조건으로 비교한다.
- uGUI는 Canvas 배치 재구성·레이아웃 갱신 비용과 드로우콜 등을 확인한다.
- UI Toolkit은 스타일·레이아웃·렌더링 관련 비용 중 실제 변경과 관련된 항목을 확인한다.
- 두 시스템 모두 화면 배치, 표시 순서, 입력, 표시·숨김과 애니메이션이 유지되는지 확인한다.
- 측정 환경과 수행 결과를 보고하고, 확인하지 못한 성능 효과는 미검증으로 남긴다.
- 성능 조사와 최적화의 공통 절차는 [성능 컨벤션](<../Safety/Performance Convention.md>), 검증 결과 보고는 [테스트·검증 컨벤션](<../Safety/Testing Convention.md>)을 따른다.

### UI 기술 참고

- [Unity 6000.3 런타임 UI](https://docs.unity.com/en-us/engine/6000.3/manual/uitoolkits/uielements/uie-support-for-runtime-ui/uie-get-started-with-runtime-ui)
- [Unity uGUI 최적화 가이드](https://unity.com/how-to/unity-ui-optimization-tips)
- [Unity 6000.3 UI Toolkit 성능](https://docs.unity.com/en-us/engine/6000.3/manual/uitoolkits/uielements/uie-support-for-runtime-ui/uie-performance-consideration-runtime)
- [Unity 6000.3 UIDocument](https://docs.unity.com/en-us/engine/6000.3/manual/uitoolkits/uielements/uie-support-for-runtime-ui/uie-render-runtime-ui/uie-create-ui-document-component)

## 제안·승인·결과 보고

- 계층 설정이 필요하면 에이전트가 역할과 필요한 기능을 분석하여 구조를 먼저 제안한다.
- 요청 대상과 구현에 필요한 범위만 보고한다. 플레이어 요청에 주변 환경이나 카메라 시스템 전체를 관련 있다는 이유만으로 포함하지 않는다.
- 새 구조를 정하거나 변경할 필요가 있을 때만 브리핑에 포함한다. 이미 승인된 구조를 유지하는 후속 작업에서는 전체 계층도를 반복하지 않는다.
- 변경 부분의 역할·컴포넌트 배치·Inspector 연결·필요한 외부 참조·풀링 반환 부모와 초기화·기존 구조 활용 여부·이름 충돌·참조와 동작 영향·검증 방법을 필요한 범위에서 보고한다.
- 새 구조와 변경 부분은 적용 전에 승인받는다. 전체 계층도는 이해에 필요한 경우에만 제시한다.
- 작업 후에는 실제 생성·삭제·수정한 계층만 보고한다. 변경이 없으면 기존 계층도를 다시 싣지 않는다.
- [승인 범위 컨벤션](<../Safety/Approval Convention.md>)에 따라 Scene·Prefab·`.meta` 변경 대상을 명시한다.

## 검증

- 부모·자식 관계, 이름과 참조 경로의 충돌, Transform·활성화 영향, Inspector 참조와 Prefab 연결을 확인한다.
- 풀링 변경은 사용 종료 후 반환 부모와 필요한 상태 초기화를 확인한다.
- UI는 해당 시스템의 실제 화면·입력·표시 순서와 변경 영향을 확인한다.
- 성능 조사는 [성능 컨벤션](<../Safety/Performance Convention.md>), 검증과 미검증 항목 보고는 [테스트·검증 컨벤션](<../Safety/Testing Convention.md>)을 따른다.
