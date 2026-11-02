---
name: diagram
description: 이 프로젝트의 다이어그램을 규칙에 맞게 Mermaid flowchart 또는 SVG 로 작성 · 갱신한다. 기술 문서에 그림이 필요할 때, 공간 · 기하 내용이라 SVG 가 더 적합하다고 판단될 때, 또는 사용자가 다이어그램 작성을 요청할 때 사용한다.
---

# 다이어그램 작성

> 이 스킬은 절차와 멘트 규격만 정의한다. 규칙의 원본은 아래 문서이며, 내용을 이 파일에 복사하지 않는다.
>
> 사용자가 따로 요청하지 않으면 PPT 용 그림은 고려하지 않는다.

## 참조 문서 (필요 섹션만 읽는다)

- 형식 기준 · SVG 규칙 · 보안 : `Docs/Conventions/Work/Technical Document Convention.md` 의 `다이어그램`, `보안`

## 절차

1. 참조 문서의 필요 섹션을 읽는다.
2. 그릴 내용을 정리하고 형식을 정한다. 기본은 Mermaid flowchart 다.
3. 공간 · 기하 내용이라 flowchart 로 표현하기 어려우면, SVG 를 이유와 함께 제안하고 승인을 기다린다. (멘트 ①)
4. Mermaid 는 문서 안에 직접 작성한다. SVG 는 원본 문서의 위치에 파일로 만들고, 문서에 상대 경로로 삽입한다.
5. 노드 이름 · 그림 안 글자가 `보안` 규칙을 지키는지 확인한다.
6. SVG 는 아래 명령으로 PNG 렌더링해 직접 보고, 글자 겹침 · 어긋남이 있으면 고친 뒤 다시 확인한다.
   `"/c/Program Files (x86)/Microsoft/Edge/Application/msedge.exe" --headless --disable-gpu --hide-scrollbars --window-size=<viewBox 너비>,<viewBox 높이> --screenshot="<임시 폴더>/<그림명>.png" "file:///<SVG 절대 경로>"`
7. 결과를 보고하고 사용자에게 확인을 요청한다. (멘트 ②)
8. 수정 요청은 바뀌는 부분만 고친다.

## 멘트 규격

① SVG 제안

```
[다이어그램] SVG 제안 : <그림 이름>
- 그릴 내용 : <한 줄>
- 이유 : <flowchart 로 표현하기 어려운 점>
- 저장 위치 : `Docs/Technical/<기능영역>/Images/<그림명>.svg`
SVG 로 그릴까요? (flowchart 로 대신할 수도 있습니다)
```

② 작성 후 보고

```
[다이어그램] <flowchart / SVG> 작성 : <문서 또는 파일 경로>
- 보안 확인 : 코드 · 메서드 · 변수 이름 미포함
(SVG 인 경우) 렌더링 확인 : <확인 결과 · 고친 내용>
(Mermaid 인 경우) 렌더링 미확인 : GitHub 또는 미리보기에서 확인해 주세요.
```
