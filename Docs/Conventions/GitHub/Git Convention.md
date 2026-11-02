# GitHub 협업 규칙

## 1. 브랜치

|브랜치|역할|
|---|---|
|main|검증된 안정 버전·출시 기준|
|main-dev|팀의 개발 통합 버전|
|작업 브랜치|Issue 단위 구현·수정|


`main`과 `main-dev`에서는 직접 기능을 개발하지 않는다. <br>
최신 `main-dev`에서 작업 브랜치를 만든 뒤 PR로 `main-dev`에 병합한다.  <br>
안정화된 버전은 별도 PR을 통해 `main-dev`에서 `main`에 반영한다.

작업 브랜치 형식은 `종류/이름/작업명`이다. 작업명에는 영문 소문자와 `-`를 사용한다.

```csharp
feat/kim/anchor-link
fix/lee/tap-movement
docs/park/stage-spec
```

종류: `feat`, `fix`, `bug`, `refac`, `docs`, `chore`, `test` 등이 있으며 목록에 없더라도 새로 작성할 수 있다. <br>
작업 브랜치는 재사용하지 않는다.

## 2. 작업 순서

```
Issue → 작업 브랜치 생성 → 작업 → Commit → Push → PR → Review → Merge
```

1. GitHub 연동은 필수이며 Issue·Commit·PR을 작업 기록의 기준으로 삼는다.
Issue는 양식에 맞게 작성한다.
작은 단위 작업을 다음 작업에 포함하는 기준과 기록은 [Workflow 규칙](<../../Codex/WorkflowRules.md>)의 `2. Github Issue`를 따른다. 아래 Issue 관련 절차는 연결된 Issue가 있을 때 적용한다.
2. 외부 도구의 갱신 대상과 절차는 [업무 흐름](<Workflow Convention.md>)을 따른다.
3. 작업 시작 시 연결된 GitHub Issue의 상태를 갱신한다. 사용자가 최신 `main-dev`에서 작업 브랜치를 직접 만든다. 에이전트는 브랜치 이름을 제안하고 변경 여부를 확인한다.
4. 관련 파일만 수정하고 의미 있는 작업 단위로 Commit한다.
5. Push 후 PR 양식에 맞게 내용을 적는다.
6. 관련 담당자가 Review한다. <br>
공용 API·Scene·Prefab을 바꾸었다면 해당 담당자의 확인을 받는다.
7. 완료 기준을 충족한 PR만 `main-dev`에 Merge한다. 연결된 Issue의 완료 여부를 확인하고, 사용 중인 외부 업무 도구가 있다면 해당 상태를 확인한다.

## 3. Commit과 PR

Commit 제목은 `[종류] 변경 내용` 형식이다.

```csharp
[Feat] Anchor 정렬 판정 구현
[Bug] 화면 밖 발판 터치 오류 수정
[Docs] 공용 API 설명 추가
```
종류는 `Feat`, `Fix`, `Bug`, `Refac`, `Docs`, `Chore`, `Test` 등을 사용하며 이또한 필요에 따라 새로 추가할 수 있다.

`Fix`는 기능 방향성 변경 또는 팀원 간 연결·연동에 따른 수정에 사용한다. <br>
`Bug`는 발견한 버그를 수정할 때 사용한다. <br>
PR 제목도 같은 형식을 권장한다.

미완성 작업은 Draft PR로 현재 상태와 남은 일을 공유한다.  <br>
특별한 이유 없이 내용 없는 PR은 만들지 않는다.

Draft PR은 완료와 Review 전까지 Merge하지 않는다.

완료되는 PR에 관련 Issue가 있으면 Closes #번호로 연결한다. <br>
아직 완료 전인 Draft PR은 Refs #번호를 적는다.

PR에는 영향받는 Script · Scene · Prefab · 설정 파일, 실행 확인 결과, 남은 문제를 적는다. <br>
사용법이 필요한 공용 기능은 설명이나 화면 자료를 추가한다.

리뷰 의견 중 반드시 고칠 문제는 이유와 재현 방법을 적어 요청한다. 수정 후 다시 확인받는다.

## 4. 충돌과 재작업

충돌이 나면 겹친 파일의 담당자와 수정 내용을 확인한 뒤 해결한다. <br>
Scene·Prefab 등을 임의로 한쪽만 반영하지 않는다.

이미 완료한 작업을 다시 수정해야 하면 기존 Issue를 Reopen하거나 새 Issue를 만들고 관련 PR을 연결한다.

Merge 후 [업무 흐름](<Workflow Convention.md>)에 따라 선택한 외부 도구를 갱신한다.


## 변경 단위와 리뷰
- 한 커밋에는 함께 설명하고 되돌릴 수 있는 작업을 묶는다.
- 무관한 서식·이동·리팩토링을 기능 변경과 임의로 섞지 않는다. [Workflow 규칙](<../../Codex/WorkflowRules.md>)에 따라 승인된 작은 단위 작업은 다음 작업에 포함할 수 있으며, 변경 내용과 검증을 PR에 구분해 적는다.
- 생성 파일·개인 설정·민감정보 포함 여부를 확인한다.
- 커밋 본문 작성 기준은 [Github Commit 작성 규칙](<../../Documentation/Github Commit Rule.md>)을 따른다.
- 필수 수정 의견은 영향과 이유를 설명하고 수정 후 다시 확인받는다.

## 협업 기준
- Issue와 PR의 Assignees는 [프로젝트 값](<../../Project Values.md>)에 기록된 사용자 GitHub 계정명으로 지정한다.
- Merge 방식은 Squash merge로 한다.
- 작성자 외 리뷰어 1명의 확인을 받는다. 1인 개발은 사용자가 최종 확인한다.
- 프로젝트에서 사용 중인 필수 CI 검사가 있다면 모두 통과해야 한다.  <br>
CI가 없는 경우 통과했다고 보고하지 않으며 합의한 수동 검증 결과를 남긴다.
- `main`·`main-dev`의 직접 Commit·Push 금지는 [프로젝트 지침](<../../../AGENTS.md>)의 절대 원칙을 따른다. 기능 변경은 PR로 진행한다.
- GitHub 저장소 주소·사용 도구·실제 적용한 보호 규칙은 [프로젝트 설정](<../../Project Configuration.md>)에 기록한다.
- 문서 기준과 실제 GitHub 설정 적용 여부는 구분한다. 실제 보호 설정을 바꿨다고 추측하지 않는다.

## 태그와 양식
기존 태그 목록은 유지한다. Commit·Issue·PR별 사용 목록과 본문은 각 [템플릿](<../../Templates>)을 따른다. <br>
Refac, Fix, Bug의 확정된 의미는 변경하지 않는다.
