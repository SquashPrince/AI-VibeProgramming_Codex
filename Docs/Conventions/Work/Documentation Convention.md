# 문서 유지 컨벤션

> 상태: 공통 운영 기준. <br>
> 대상: Unity/C# 기반 AI 보조 개발. 기존 승인 규칙과 사용자 확정 사항을 유지한다.

## 목적
사양·구현·실제 검증 결과를 일치시키면서 불필요한 기록을 줄인다.

## 문서 역할
- `AGENTS.md`: AI 진입점·절대 원칙·상시 로드/온디맨드 문서 참조표
- `Docs/Codex/`: AI 공통 지침(`AgentCommonRules.md`), 행동 규칙(`CodexCodeRules.md`), 업무 프로세스(`WorkflowRules.md`)
- `Docs/GroundRule.md`: 사용자·AI·팀의 운영 원칙
- `Docs/Project Values.md`: 확정된 프로젝트 값 (상시 로드)
- `Docs/Project Configuration.md`: 미확정 프로젝트 값·착수 확인 절차
- `Docs/Conventions/`: 주제별 상세 기준 (`Code/`, `GitHub/`, `Graphics/`, `Safety/`, `Work/`)
- `Docs/Conventions/Graphics/`: 그래픽 공통 설명과 역할, Shader·HLSL·Shader Graph·Material·Texture·Sprite별 작업·최적화 기준. 진입점은 [그래픽 공통 컨벤션](<../Graphics/Common Convention.md>)이다.
- `Docs/Conventions/Animation/`: 애니메이션 공통 판단과 역할, 2D 및 3D 분야별 구성과 연결 기준. 진입점은 [애니메이션 공통 컨벤션](<../Animation/Common Convention.md>)이다.
- `Docs/Conventions/Work/Hierarchy Convention.md`: Scene·Prefab·UI 계층 구성과 폴더를 포함한 공통 계층 명명 기준
- `Docs/Documentation/`: Github Commit·Issue·PR 작성 규칙
- `Docs/Templates/`: Commit·Issue·PR·작업 일지·기술 문서 양식
- `Docs/Technical/`: 기능 단위 기술 문서와 다이어그램 ([기술 문서 컨벤션](<Technical Document Convention.md>))
- `.agents/skills/`: 반복 절차를 수행하는 스킬
- Issue·PR: 작업 목적·변경·검증·논의의 기준 기록
- 작업 일지: 하루의 실제 작업·문제·다음 작업 (`Docs/DailyLogs/`)
- `Docs/Temp/Animation-Rules-Handoff.md`: 애니메이션 규칙 작업의 승인 기록과 남은 논의를 전달하는 임시 인수인계 문서
## 갱신 시점
- 공개 API·데이터 구조·설정·연결 방법 변경 시 관련 문서를 해당 작업에서 함께 갱신한다.
- 미확정 내용은 가정 또는 미정으로 표시한다.
- 설계 선택은 관련 Issue/PR에 이유·대안·영향을 남긴다. 별도 결정 기록 문서 도입은 추후 정한다.
- 외부 업무 도구 갱신은 [업무 흐름 컨벤션](<../GitHub/Workflow Convention.md>)을 따른다.
- 작업 일지 작성과 누락 확인은 `worklog` 스킬로 수행한다.
- 작업한 날은 17:30~18:00에 작업 일지를 작성하고 사용자에게 보고한다. 시간대와 AI 비실행 시 별도 기준은 추가하지 않는다.
- 다음 작업 시작 때 이전 작업일의 일지 누락을 확인한다. 작업 기록이 없는 날은 작업하지 않은 날로 판단하고 일지 작성을 생략한다.
- 작업한 지난 날짜에 일지가 없으면 작성할지 한 번만 질문한다. 사용자가 별도로 요청하기 전에는 같은 날짜의 누락을 다시 확인하거나 작성 권유를 반복하지 않는다.
- 작업 일지를 작성한 뒤 추가로 작업하더라도 해당 일지를 수정·갱신하지 않고 다음 날 작성하는 일지에 포함한다.
- 과거 내역은 확인 가능한 기록으로 보완하고 추측하지 않는다.

## 공통 작성 형식

- 새로 작성하거나 수정하는 문서는 가운데점(`·`)으로 항목을 나열하지 않고 Markdown 리스트로 구분한다. 규칙 문서, 기술 문서, 템플릿, 작업 일지와 GitHub Issue 및 PR 본문에 공통 적용한다.
- 여러 대상을 소개할 때는 요약 문장 다음에 대상별 리스트를 작성한다. 목록의 의미를 바꾸거나 단순히 다른 구분자로 치환하지 않는다.
- 코드 식별자, 파일 경로와 정확한 원문 인용은 원래 표기를 유지한다.
- 기존 문서를 이 규칙만으로 일괄 변경하지 않는다. 새로 작성하거나 수정하는 부분부터 적용한다.

### 작성 예시

그래픽 공통 문서와 분야별 규칙 문서 7개를 작성했다.

- 공통
- Shader
- HLSL
- Shader Graph
- Material
- Texture
- Sprite

## 유지 규칙
- 파일 경로·용어·태그와 관련 예시를 함께 확인한다.
- 불일치가 있으면 위치와 영향을 알리고 기준 확정 후 정리한다.
- 필독 진입점은 AGENTS.md에서 관리하고, 문서를 추가·분리·삭제하면 AGENTS.md와 이 문서의 `문서 역할`을 함께 갱신한다.
