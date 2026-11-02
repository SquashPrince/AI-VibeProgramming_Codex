---
name: github-pr
description: 이 프로젝트의 Github Pull Request 를 규칙과 양식에 맞게 작성 · 등록한다. 사용자가 Push 를 마쳐 PR 단계에 들어갈 때, 또는 사용자가 PR 작성을 요청할 때 사용한다.
---

# Github PR 등록

> 이 스킬은 절차와 멘트 규격만 정의한다. 규칙의 원본은 아래 문서이며, 내용을 이 파일에 복사하지 않는다.

## 참조 문서 (필요 섹션만 읽는다)

- 업무 흐름 : `Docs/Codex/WorkflowRules.md` 의 `8. PR`
- 제목 · 섹션 · Label 규칙 : `Docs/Documentation/Github PR Rule.md`
- 본문 양식 : `Docs/Templates/PullRequest.md`
- 대상 브랜치 · Issue 연결 · Draft PR : `Docs/Conventions/GitHub/Git Convention.md` 의 `1. 브랜치`, `3. Commit과 PR`
- Assignees : `Docs/Conventions/GitHub/Git Convention.md` 의 `협업 기준`

## 절차

1. 참조 문서의 필요 섹션을 읽는다.
2. 현재 브랜치와 Push 여부를 확인한다. `main`, `main-dev` 이거나 Push 되지 않았으면 등록하지 않고 사용자에게 알린다.
3. 사용자에게 PR 등록을 권유한다. (멘트 ①)
4. 관련 Issue 와 `main-dev` 대비 변경 파일을 확인한다. 관련 Issue 가 없으면 사용자에게 확인한다.
5. 양식에 맞춰 초안을 작성한다. 함께 수행한 작은 단위 작업은 본 작업과 구분해 적는다. `확인 결과`는 실제로 수행해 확인한 항목만 체크하고, 실행하지 않은 항목은 미검증으로 남긴다.
6. 초안을 보여주고 승인을 받는다. (멘트 ②)
7. 승인 후 본문을 스크래치패드 파일로 작성하고 `gh pr create --base main-dev --title --label --assignee --body-file` 로 등록한다.
8. 등록 결과를 보고한다. (멘트 ③)

## 멘트 규격

① PR 등록 권유

```
[PR] PR 등록이 가능합니다.
- 브랜치 : <작업 브랜치> → main-dev
- 관련 Issue : #<번호>
PR 초안을 작성할까요?
```

② 등록 전 승인 요청

```
## PR 초안 : <제목>
- 브랜치 : <작업 브랜치> → main-dev
- 연결 : Closes #<번호>   |   Label : <Label>

(본문 : 관련 Issue · 구현 내용 · 영향받는 파일 · 확인 결과 · 특이사항)

**확인 필요** : 사용자 답변 없이 보충한 항목 (없으면 생략)
등록할까요?
```

③ 등록 후 보고

```
PR #<번호> 등록 완료 : <링크>
다음 단계 : 리뷰 후 Squash merge · 브랜치 삭제 (사용자 직접 수행)
```
