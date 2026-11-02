---
name: github-issue
description: 이 프로젝트의 Github Issue 를 규칙과 양식에 맞게 작성 · 등록한다. 개발 · 버그수정 요청으로 Issue 를 작성할 때, 또는 사용자가 이슈 작성을 요청할 때 사용한다.
---

# Github Issue 등록

> 이 스킬은 절차만 정의한다. 규칙의 원본은 아래 문서이며, 내용을 이 파일에 복사하지 않는다.

## 참조 문서

- 업무 흐름 : `Docs/Codex/WorkflowRules.md` 의 `2. Github Issue`
- 제목 · 섹션 · Label 규칙 : `Docs/Documentation/Github Issue Rule.md`
- 본문 양식 : `Docs/Templates/Issue.md`
- Assignees : `Docs/Conventions/GitHub/Git Convention.md` 의 `협업 기준`

## 절차

1. 참조 문서 3개를 읽는다.
2. 사용자 요청·승인된 브리핑·기존 기록에서 `배경 / 목적`, `목표`, `작업 내용`, `완료조건`, `Label`을 확인한다. 이미 제공된 내용은 다시 묻지 않는다.
3. 빠진 항목만 사용자에게 질문한다. `배경 / 목적`에 대한 근거를 확인하지 못하면 등록하지 않고, 다른 항목도 추측으로 채우지 않는다.
4. 관련 코드 · 파일이 이미 있으면 확인하고, 답변과 다른 점이 있으면 알린다.
5. `gh label list` 로 Label 존재 여부를 확인한다. 없으면 생성 여부를 초안과 함께 묻는다.
6. 초안을 사용자에게 보여주고 승인을 받는다. 사용자 답변 외에 보충한 부분은 따로 표시한다.
7. 승인 후 본문을 스크래치패드 파일로 작성하고 `gh issue create --title --label --assignee --body-file` 로 등록한다.

## 등록 후 보고

- Issue 번호
- Issue 링크
- 다음 단계 : 사용자의 착수명령을 기다린다는 안내
