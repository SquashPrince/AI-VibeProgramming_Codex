# Github Issues 작성 규칙

> Github Issues를 작성할 때의 규칙.

> 이슈 작성 양식은 [Issue 템플릿](<../Templates/Issue.md>)을 참고.

> 해당 문서는 제목, 각 섹션, Label에 대한 작성 규칙을 정의한다.

## 0. 작성 절차

- 아래에 해당하면 Issue 작성을 권유한다.
    - 개발 · 버그수정 요청
    - 여러 파일 또는 여러 단계에 걸친 작업
    - 다른 Issue와 연관된 작업
- [Workflow 규칙](<../Codex/WorkflowRules.md>)의 작은 단위 작업은 다음 작업에 포함하고, 별도 Issue 권유를 생략한다.
- 사용자가 Issue를 만들지 않도록 지시한 경우, 작성하지 않는다.
- 필요한 내용은 사용자에게 질문하여 채우며, 추측으로 작성하지 않는다.
- 작성 전 Issue 초안을 사용자에게 보여주고 승인 후 등록한다.
- Issue의 Assignees는 [Git 컨벤션](<../Conventions/GitHub/Git Convention.md>)의 협업 기준을 따른다.

## 1. 제목

- 제목은 `[태그] 작업 내용` 형식으로 작성
- 태그는 [Git 컨벤션](<../Conventions/GitHub/Git Convention.md>)의 Commit 종류를 따른다.
- 작업 내용은 작업의 요약을 명사형으로 끝나도록 작성한다.
- 제목에는 괄호 `()` 를 사용하지 않는다.
- 예시 : 
    ```markdown
    [Feat] 플레이어 이동 구현
    ```

## 2. 배경 / 목적

- 이슈 작성 시 사용자 요청·승인된 브리핑·기존 기록에서 해당 섹션의 근거를 확인한다. 이미 제공된 내용은 다시 묻지 않고, 빠진 내용만 질문한다. 배경과 목적의 근거가 확인되지 않으면 이슈를 등록하지 않는다.
- 리스트로 작성하며, 불필요한 표현을 제거해 간결하게 작성한다.
- 예시 :
    ```markdown
    ## 배경 / 목적
    - 플레이어 피격 판정 시, 데미지 수치와 공격판정에 필요한 데이터 동시 전달 필요
    - `DamageInfo` 구조체를 전달하는 방식으로 피격 판정 개선
    ```

## 3. 목표 (To-be)

- 작업이 완료 되었을 때의 예상 동작 혹은 결과를 기재한다.
- 리스트로 작성하며, 불필요한 표현을 제거해 간결하게 작성한다.
- 예시 :
    ```markdown
    ## 목표 (To-be)
    - 플레이어와 몬스터간 공격판정 시, `IDamageable` 인터페이스의 `TakeDamage(DamageInfo info)`를
    통해 공격 판정 개선
    - `DamageInfo` 구조체가 데미지를 주는 객체로부터 받는 객체로 전달.
    ```

## 4. 작업 내용

- 예상되는 프로젝트의 생성/수정/삭제에 대한 변경사항을 기재한다.

- 리스트로 작성하며, 특정 경로의 파일에 어떤 작업이 이뤄지는지 기재한다.
- 예시 :
    ```markdown
    ## 작업 내용
    - `Assets/Scripts/PlayerController.cs` 수정
    - `Assets/Scripts/Core/IDamageable.cs` 수정
    - `Assets/Scripts/Monster/Goblin.cs` 생성
    - `Assets/Scripts/Monster/Monster.cs` 수정
    ```

## 5. 완료조건 (DoD)

여부를 체크리스트로 둔다.
- 예시 :
    ```markdown
    ## 완료조건 (DoD)

    - [ ] `PlayerController` 객체와 `MonsterController` 객체가 `IDamageable` 인터페이스를 통해서만 데미지 판정을 주고받는다.
    - [ ] `IDamageable.TakeDamage(DamageInfo info)` 메서드를 통해 공격을 시도한
    객체에서 공격을 받는 객체로 `DamageInfo` 구조체가 전달된다.
    - [ ] 공격을 받는 객체에서 `DamageInfo` 구조체의 정보를 받아 로그 출력에
    성공했다.
    - [ ] 유니티 에디터에서 컴파일 에러 및 Error, Warning 로그가 발생하지 않는다.
    ```

## 6. 특이사항

- 아래의 사항에 해당되지 않으면 이 섹션은 기재하지 않는다.
    1. 해당 이슈가 진행되기 위한 선행 조건이 있는 경우.
    2. `Github Issues`에 등록된 다른 Issue와 연관되어 있는 경우
    3. 해당 이슈를 통해 해소된 블로커 및 이슈가 있는 경우.
- `Github Issues`에 등록된 Issue를 언급하는 경우, 태그를 걸어둔다.

## 7. 기타

### Label

Label은 아래 항목중 하나를 채택하여 걸어둔다.
- Bug : 오류 메시지가 뜨거나 무언가 예상과 다르게 작동할 때
- Docs : 문서관련 작업을 수행
- Feature : 새로운 기능을 추가
- Improvement : 기존 기능을 개선하거나 보수하는 작업
- Art : 그래픽 및 에셋 제작
- UI/UX : UI 및 사용자 경험 작업
- Refactor : 기능의 변화를 주지 않는 코드 개선
- Chore : 기타 유지보수 작업, 설정 및 패키지 정리
