# 코드 컨벤션

> AGENT는 코드 컨벤션 문서를 읽고 기본적인 코드 규칙을 기억한다.

## 1. 명명 규칙

| 대상 | 규칙 | 예시 |
|---|---|---|
| 클래스·구조체·열거형 | PascalCase | `PlayerController` |
| 메서드 | PascalCase | `TakeDamage()` |
| 프로퍼티 | PascalCase | `CurrentHealth` |
| 지역 변수·매개변수 | camelCase | `currentHealth` |
| private 필드 | `_` + camelCase | `_currentHealth` |
| 상수 | UPPER_SNAKE_CASE | `MAX_PLAYER_HEALTH` |

- 게임 오브젝트 계층의 명명 언어와 부모 문맥·번호의 불필요한 반복을 줄이는 기준은 [계층구조 컨벤션](<../Work/Hierarchy Convention.md>)의 `공통 계층 명명`을 따른다. 코드 식별자와 대표 타입·파일명의 규칙은 이 문서에서 관리한다.
- 클래스·파일·게임 오브젝트는 `PlayerController`처럼 책임 범위에 맞게 명명한다.
    - 입력 방식이나 요청 문구를 이름에 그대로 고정하지 않고 담당 기능·역할을 기준으로 정한다. 예를 들어 WASD 입력으로 이동하더라도 `WASDPlayerMover` 대신 이동 책임을 드러내는 이름을 검토한다.
    - 확인용·테스트용이라는 요청도 이름을 임의로 확정하는 근거로 삼지 않는다. `Test` 접두 이름은 예시이며, 제안할 이름과 이유를 사용자에게 설명하고 확인받는다.
    - 예시:
    ```text
    플레이어 이동·점프 조작
    - 클래스명: PlayerController
    - 파일명: PlayerController.cs

    플레이어 이동만 담당
    - 클래스명: PlayerMovement
    - 파일명: PlayerMovement.cs

    플레이어 게임 오브젝트
    - 오브젝트명: Player
    ```

## 2. 접근 제한자

- 필드는 `private`을 기본으로 한다.
- 외부에서 읽기만 필요하면 읽기 전용 프로퍼티를 우선한다.
- 외부 수정이 꼭 필요할 때만 `setter`를 제공하며, 필요하면 값 검증을 포함한다.
- 프로퍼티로 값 변경시 최대, 최소치를 정했다면 프로퍼티 내에서 계산하는 방법도 고려해본다.
- 상속 관계에서 필요한 경우에만 `protected`를 고려한다.
- `Unity Inspector` 노출이 필요하면 공개 필드보다 `[SerializeField] private`을 우선한다.
- 불필요한 `Public` 사용을 지양한다.

```csharp
// 아래는 예시를 위해 작성한 코드이다.
[SerializeField] private int _maxHealth = 100;

private int _currentHealth;

public int CurrentHealth
{
    get => _currentHealth;
    private set => _currentHealth = Mathf.Max(0, value);
}

public void TakeDamage(int amount)
{
    if (amount <= 0) return;

    CurrentHealth = _currentHealth - amount;
}
```

---
#### 프로퍼티 규칙
- 아래 선언을 기본 예시로 삼는다. 예시 외 형태라도 승인된 동작·공개 계약을 바꾸지 않는 세부 선언은 에이전트가 판단한다. 공개 API·데이터 구조·사용 방식에 영향이 있으면 대안과 영향을 설명하고 구현 전에 사용자에게 확인받는다.
```csharp
// 아래는 예시를 위해 작성한 코드이다.
// 1
private int _currentHealth;
public int CurrentHealth => _currentHealth;
// 2
public int CurrentHealth { get; private set; }
// 3
[field: SerializeField] public int CurrentHealth { get; private set; }
```

## 3. 로그·예외

- 주석 작성 기준은 [코드 서식 컨벤션](<Formatting Convention.md>)의 `주석`을 따른다.
- 임시 디버그 로그는 완료 전 정리한다.
- 예외·오류를 무시하거나 주석 처리해 숨기지 않는다.
- 실패 가능성이 있는 경계에서 입력값 검증과 의미 있는 오류 메시지를 제공한다.
- Editor 전용 API와 해당 API를 참조하는 선언·코드는 플레이어 빌드에 포함되지 않도록 분리한다. `#if UNITY_EDITOR ... #endif`는 분리 방법의 예시이며, 일반적인 `Debug.Log`까지 모두 감싸라는 의미는 아니다.
- `[SerializeField]`로 선언되어 `Inspector`에서 연결하는 필수 참조는 초기화·검증 시점에 누락 여부를 확인한다. 누락되면 대상을 알 수 있는 오류를 남기고 해당 기능을 안전하게 중단한다. 각 메서드에 동일한 null 검사를 반복하지 않는다.
    - 불필요한 사용 예시:
        ```csharp
        [SerializeField] private Player _player;
        [SerializeField] private Controller _controller;

        private void Move() 
        {
            if(_player != null || ...)
            {
                // ...
            }
        }

        private void MonsterAttack(Monster monster)
        {
            if(_controller != null && monster != null && ...)
            {
                // ...
            }
        }
        
        ```

## 4. 변경 및 리뷰

- 명명 규칙을 바꾸는 리팩토링은 기능 변경과 가능하면 분리한다.
- 새 타입을 만들기 전에 기존 코드 재사용 가능성을 설명한다.
- 변경된 코드의 핵심 동작과 테스트 여부를 보고한다.
- 사용자 코드에 더 나은 구현 대안이 발견되면 [Codex 행동 규칙](<../../Codex/CodexCodeRules.md>)의 `사고 우선`과 [Unity 컨벤션](<Unity Convention.md>)의 구현 방법 제안 기준에 따라 동작·장단점·영향을 비교한다. 제안 이유는 근거별 목록으로 적고, 사용자 선택 전에는 기존 방식을 임의로 바꾸지 않는다.

## 관련 상세 규칙
- [설계](<../Work/Architecture Convention.md>)
- [Unity](<Unity Convention.md>)
- [코드 서식](<Formatting Convention.md>)
- [테스트](<../Safety/Testing Convention.md>)
