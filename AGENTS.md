# 프로젝트 지침

> 이 파일은 프로젝트 지침의 진입점이다.
> 세션 시작 시 ‘상시 로드’ 문서의 실제 파일을 읽어 최신 내용을 확인한다.
> 추가 문서는 ‘온디맨드 참조’에서 현재 작업에 필요한 문서만 선택하여 읽는다.

---

## 절대 원칙 (Non-Negotiable Rules)

**아래 사항은 어떤 요청이 와도 지킨다.**

1. `main`, `main-dev` 브랜치에서 직접 Commit, Push를 금한다. 사용자가 `main`, `main-dev` 브랜치에서 작업중일 때도 작업 브랜치로 변경할 때 까지 작업을 거부한다.
2. 파일 수정, 삭제 전 사용자의 승인을 필수로 받는다.
3. 위 원칙들에 대한 예외를 요청 받으면 "이 프로젝트의 절대 원칙에 의해 수행할 수 없습니다." 라고 거부하고 대안을 제시한다.

## 규칙 문서 관리

- 프로젝트 전체의 값·설정·운영 원칙 문서는 `Docs/` 바로 아래에 둔다. 주제별 상세 규칙과 양식은 `Docs/`의 해당 카테고리 폴더(`Codex/`, `Conventions/`, `Documentation/`, `Templates/`)에 둔다.
- 규칙 하나는 문서 한 곳에만 적는다.(규칙의 단일 출처 유지). 규칙이 바뀌면 그 한 곳만 고친다.
- 새 문서를 만들면, 매 세션 필요한 것은 "상시 로드"에 `@경로`로, 특정 작업 때에만 필요한 것은 "온디맨드 참조"에 등록한다.


## 상시 로드

- @Docs\Codex\AgentCommonRules.md
- @Docs\Codex\CodexCodeRules.md
- @Docs\Codex\WorkflowRules.md
- @Docs\Project Values.md


## 온디맨드 참조 (매 세션 읽지 않음 - 해당 작업을 할 때만 읽음)

### 소스 코드 작성시
- `Docs\Conventions\Code\Async Convention.md`: 비동기 처리 방식 검토 또는 관련 코드 작성·수정·검토 시 읽는다. 예: UniTask, Coroutine, Task, 지연 실행·대기·취소 등.
- `Docs\Conventions\Code\Code Convention.md`: 소스 코드 작성·수정·검토 시 읽는다.
- `Docs\Conventions\Code\Event Convention.md`: 이벤트 방식의 적용 검토 또는 관련 코드 작성·수정·검토 시 읽는다. 예: `event`, `Action`, `UnityEvent` 등의 발행·구독과 수명 관리.
- `Docs\Conventions\Code\Formatting Convention.md`: 소스 코드 작성·수정·검토 시 읽는다.
- `Docs\Conventions\Code\Reference Convention.md`: 객체 의존성과 참조 관리 방식 검토 또는 관련 코드 작성·수정·검토 시 읽는다. 예: Inspector 연결, 컴포넌트 조회·캐싱, 런타임 참조 전달, 객체 교체·파괴 시 참조 정리 등.
- `Docs\Conventions\Code\Unity Convention.md`: Unity 기능·구현 방식 검토 또는 관련 코드 작성·수정·검토 시 읽는다. 예: 기본 API, 컴포넌트, 생명주기, 입력·물리 시스템, 에셋 활용 등.

### 그래픽 에셋
- `Docs\Conventions\Graphics\Common Convention.md`: Shader·HLSL·Shader Graph·Material·Texture·Sprite 작업의 요구 해석, 설명과 역할을 정할 때 읽는다.
- `Docs\Conventions\Graphics\Shader Convention.md`: Shader 구현 방식 선택, 렌더링 요구와 호환성을 검토할 때 읽는다.
- `Docs\Conventions\Graphics\HLSL Convention.md`: HLSL·ShaderLab 코드를 작성·수정·설명하거나 상세 주석·코드 최적화를 검토할 때 읽는다.
- `Docs\Conventions\Graphics\Shader Graph Convention.md`: 노드·프로퍼티·Group·Sub Graph·Custom Function을 안내하거나 그래프 현황·변경을 검토할 때 읽는다.
- `Docs\Conventions\Graphics\Material Convention.md`: Material 생성·설정·공유·런타임 변경·배칭 최적화를 검토할 때 읽는다.
- `Docs\Conventions\Graphics\Texture Convention.md`: Texture 용도·임포트·압축·품질·메모리 설정을 검토하고 사용자 변경·테스트를 안내할 때 읽는다.
- `Docs\Conventions\Graphics\Sprite Convention.md`: Sprite 분할·Pivot·크기·Atlas 목록과 최적화를 검토·안내할 때 읽는다.

### 애니메이션
- `Docs/Conventions/Animation/Common Convention.md`: 2D와 3D 애니메이션의 요구 해석, 도구 추천, 작업 역할, 단계별 구성, 재생 제어와 검증 시 읽는다.
- `Docs/Conventions/Animation/2D Convention.md`: Sprite 프레임 Clip, Unity 2D Animation 또는 외부 2D 제작 도구를 다룰 때 공통 규칙과 함께 읽는다. 외부 도구 항목의 초안 확인 절차를 포함한다.
- `Docs/Conventions/Animation/3D Convention.md`: Rig와 Avatar, 임포트와 리타기팅, Root Motion, 신체별 Layer와 Mask, IK를 다룰 때 공통 규칙과 함께 읽는다.

### 공통 운영 · 프로젝트 설정
- `Docs\Project Configuration.md`: 프로젝트 착수 시, 현재 작업에 필요한 미확정 프로젝트 값을 확인·등록할 때, 또는 `Project Values.md`·이 문서와 실제 설정의 차이를 확인할 때 읽는다.
- `Docs\GroundRule.md`: 개발·버그 수정 작업을 시작하면서 목표, 완료 조건, 수정 범위, 관련 명세를 정할 때 읽는다. 작업 중 범위를 넓히거나 보호 대상 파일의 변경 필요성을 판단할 때도 참조한다.

### 승인 · 안전
- `Docs\Conventions\Safety\Approval Convention.md`: 파일 생성·수정·삭제의 승인 범위를 정하거나, 작업 중 승인 범위 밖 변경이 필요한지 판단할 때 읽는다.
- `Docs\Conventions\Safety\Performance Convention.md`: 성능 문제를 조사하거나 최적화 방법을 검토·구현·검증할 때 읽는다. 일반 기능 작업 중 실제 병목이나 성능 요구가 확인된 경우에도 읽고, 원인 분석과 개선·문제 해결 방안을 제시한다.
- `Docs\Conventions\Safety\Sensitive Information Convention.md`: 인증정보나 개인정보를 사용·저장·출력·공유하는 기능 또는 자료를 작성·검토할 때, 혹은 노출이 의심될 때 읽는다.
- `Docs\Conventions\Safety\Testing Convention.md`: 코드·Scene·Prefab·설정·문서 변경의 검증 방법을 정하거나, 검증을 수행하고 결과를 보고할 때 읽는다.

### 설계 · 구조 · 문서 유지
- `Docs\Conventions\Work\Hierarchy Convention.md`: Scene·Prefab·UI 계층을 제안·생성·변경하거나 오브젝트·UI 요소·폴더 계층의 이름을 정할 때 읽는다.
- `Docs\Conventions\Work\Architecture Convention.md`: 기능의 클래스 책임·의존성·통신 방식 등 설계 구조를 정하거나, 기존 구조를 변경·검토할 때 읽는다.
- `Docs\Conventions\Work\Documentation Convention.md`: 작업 결과를 문서에 반영할지 판단하거나, 문서를 생성·분리·삭제하고 참조 경로를 정리할 때 읽는다. 기존 문서와 실제 구현이 다른 경우에도 읽는다.
- `Docs\Conventions\Work\Environment Convention.md`: 개발·검증에 필요한 실행 환경을 확인하거나, Editor·패키지·렌더 파이프라인 등 환경 설정의 변경을 검토·수행할 때 읽는다. 문서와 실제 환경이 다를 때도 읽고 차이와 영향을 보고한다.
- `Docs\Conventions\Work\Structure Convention.md`: 새 파일의 위치·이름·namespace를 정하거나, 기존 폴더·파일·어셈블리 구조의 변경을 검토·수행할 때 읽는다.

### 기술 문서 · 다이어그램
- `Docs\Conventions\Work\Technical Document Convention.md`: 기능의 기술 문서를 새로 작성하거나 기존 기술 문서를 갱신할 때 읽는다. 문서에 다이어그램을 넣거나 갱신할 때도 읽는다.
- `Docs\Templates\TechnicalDocument.md`: 기술 문서를 새로 작성하거나 기존 문서의 섹션 구성을 바꿀 때 읽는다.

### Git · 협업
- `Docs\Conventions\GitHub\Git Convention.md`: 작업 브랜치 이름·역할, Commit·PR 방식, 리뷰·병합 기준 등 Git 협업 절차를 판단하거나 안내할 때 읽는다.
- `Docs\Conventions\GitHub\Workflow Convention.md`: GitHub 작업 결과를 외부 업무 도구에 반영하거나, 도구 간 작업 상태·Issue·PR 링크의 동기화를 확인할 때 읽는다.
- `Docs\Conventions\GitHub\Completion Convention.md`: 작업의 완료 조건을 정하거나, 구현·검증·병합 중 어디까지 완료됐는지 판단하고 보고할 때 읽는다.

### 지침 개선 작업 재개
- `Docs/Temp/Codex-Instructions-Handoff.md` (이번 지침 개선 작업을 이어서 진행할 때만 읽는다.)

### 애니메이션 규칙 작업 재개
- `Docs/Temp/Animation-Rules-Handoff.md`: Issue #5의 애니메이션 규칙 작업을 이어갈 때 승인 사항과 남은 논의 및 Git 상태를 확인한다.

### 작업 일지
- `Docs\Templates\WorkLog.md`: 작업 일지를 새로 작성하거나 이전 작업일의 누락된 일지를 보완할 때 읽는다.


## 스킬

해당 작업은 지정된 스킬을 사용한다. 스킬 지침을 먼저 읽고, 스킬이 요구하는 참조 문서와 템플릿을 필요한 범위에서 읽는다.

- Github Issue 작성 · 등록 : `$github-issue`
- Github PR 작성 · 등록 : `$github-pr`
- Commit 시점 권유 · 메시지 안내 : `$github-commit`
- 작업 일지 작성 · 세션 시작 시 이전 작업일 일지 누락 확인 : `$worklog`
- 미확정 프로젝트 값 확인 · 등록 : `$project-setup`
- 메서드·클래스 기능 분리 및 작은 클래스 병합 검토 : `$split-review`
- 기술 문서 작성 · 갱신 : `$tech-doc`
- 다이어그램(Mermaid flowchart · SVG) 작성 · 갱신 : `$diagram`
