# 코드 서식 컨벤션

## 목적
> AGENT는 이 컨벤션을 참고해 서식 차이를 줄이고 동작 변경을 쉽게 검토한다.
>
> 사용자의 코드 서식과 **최대한** 맞출수 있도록 유지한다.
> 
> 이미 작성된 코드를 임의로 변경하지 않으며 사용자의 명시적인 요구가 있을 경우 검토하고 진행한다.
>
> 수정하는 부분은 주변 코드의 서식을 따른다. 작업과 무관한 기존 서식 차이는 임의로 고치거나 매번 질문하지 않는다. 요청한 변경을 진행하는 데 서식 기준의 결정이 실제로 필요할 때만 차이와 선택지를 보고한다.

## 기본 서식  
- 공백 4칸으로 들여쓴다.
- 타입·메서드·제어문 중괄호는 다음 줄에 배치한다.
- 조건문에서 조건이 여러개라도 **되도록 한 줄로 이어서** 적으며, 수식형 조건이 3개 이상일 경우 줄바꿈이나 내부 if문을 만들어 너무 길어지지 않게 한다.
    - 예시 : 
    ```csharp
    if(_player.CurrentHealth >= 200 || _monster.AttackValue == 10 && GameManager.Instance.CanMonsterAttack)
    {
        // ...
    }

    if(_player.CurrentHealth >= 200 ||
     _monster.AttackValue == 10 &&
      GameManager.Instance.CanMonsterAttack)
    {
        // ...
    }
    ```
- 판단 기준이 애매하다고 판단될 경우 임의로 진행하지 말고 사용자에게 먼저 확인을 받은후 비슷한 상황에서도 같은 기준으로 처리하는지 답변 받아 기억한다.
- 여러 줄의 제어문 본문에는 중괄호를 사용한다. 한 줄 조기 반환은 예시같은 상황에서 사용한다.
    - 예시 :
    ```csharp
    if(A == B)
    {
        if(A != C) return;

        int T = B;

        B = A; 
        
        A = T;
    }
    ```
- 오른쪽 식에서 타입이 분명하더라도 var 사용을 최소화 한다.
- 의미가 다른 처리 단계 사이에는 빈 줄 하나를 둔다.
- `Update()`의 전체 동작이 단일 표현식 한 줄로 끝나면 식 본문으로 직접 적을 수 있다. 예: `private void Update() => c = a + b;`. 그 외에는 처리 내용을 별도 메서드로 분리하고 `Update()`에서 호출만 한다.
- 참조 캐싱과 초기 상태 설정은 `Awake()`·`Start()` 안에 직접 적지 않고, 스크립트 최하단의 `CacheComponent()`(참조 캐싱)와 `Init()`(초기 상태 설정)으로 나눈 뒤 호출만 한다.
- event 관련 구독, 취소는 각각 Bind[Xxx]Event(), Unbind[Xxx]Event() 이름 형식의 함수를 만들고, OnEnable(), OnDisable()에서 호출만 하도록한다.
- Unity 이벤트 함수에서 호출할 메서드가 하나라면 중괄호 대신 `=>` 사용하여 호출한다.
- 기능 수정에 무관한 서식 변경을 섞지 않는다.

## 멤버 순서
1. 정적 필드
2. 상수 필드
3. Inspector 필드
4. 일반 private 필드
5. 일반 public 필드
6. 프로퍼티·이벤트
7. Unity 이벤트 함수
8. 공개 동작 메서드·보조 메서드
9. CacheComponent(), Init()

※ 같은 필드 맴버 라면 `리스트 -> 클래스 -> 넘버릭` 과 같은 순서로 작성한다.

- 예시:
```csharp
public static GameManager Instance;

public const float GAME_TIME = 300f;

[SerializeField] private float _currentTime;

private Player _player;

public List<Monster> MonsterList;
public Monster TargetMonster;
public int MonsterCount;

public int AttackValue => _player.AttackValue;
public float CurrentTime
{
    get
    {
        return _currentTime;
    }
    private set
    {
        _currentTime = value;

        if(_currentTime <= 0f)
        {
            _currentTime = 0f;
        }
    }
}

private void Awake() => CacheComponent();
private void Start() => Init();
private void OnEnable() => BindPlayerMoveEvent();
private void OnDisable() => UnbindPlayerMoveEvent();
private void Update()
{
    Move();
    Jump();
}

private void Move()
{
    // ..
}

private void Jump()
{
    // ..
}

public void GetPlayerCurrentHealth()
{
    // ..
}

// ..

private void CacheComponent()
{
    // ..
}

private void Init()
{
    // ..
}

private void BindPlayerMoveEvent()
{
    // ..
}

private void UnbindPlayerMoveEvent()
{
    // ..
}

```

## 기능 분리

> 검토 · 보고는 `split-review` 스킬로 수행한다.
> 줄 수는 실제 코드가 있는 줄만 센다. 빈 줄·주석만 있는 줄과 중괄호·소괄호·대괄호 등 표시용 괄호만 있는 줄은 제외한다. 식별자나 값이 있는 줄은 센다. `if` 조건과 실제 본문 코드가 각각 한 줄이면 2줄로 센다.

- 하나의 메서드가 실제 코드 40줄 이상이면 기능 분리를 검토한다. 줄 수만으로 분리를 결정하지 않는다.
- 기능 분리시 기존 동작은 유지 되어야 하며, 외부 호출에서도 영향을 받지 않아야 한다.
- 사용자에게 다음과 같이 보고 하고 답변을 받아 진행한다.
    ```markdown
    ## 기능 분리 검토
    - 대상 : `대상 메서드의 이름과 소속 클래스`
    - 기능 분리 근거 : ...
    - 기존 메서드에서 유지할 기능과 분리된 기능 :
        - OriginMethod() : `유지할 기능`
        - AddonMethod() : `분리된 기능`
    [메서드] 기능 분리에 승인하시겠습니까?
    ```

- 하나의 클래스가 실제 코드 250줄 이상이면 기능 분리를 검토한다. 줄 수만으로 분리를 결정하지 않는다.
- 기능 분리시 기존 동작은 유지 되어야 하며, 외부 호출에서도 영향을 받지 않아야 한다.
- 사용자에게 다음과 같이 보고 하고 답변을 받아 진행한다.
    ```markdown
    ## 기능 분리 검토
    - 대상 : `대상 클래스`
    - 기능 분리 근거 : ...
    - 기존 클래스에서 유지할 기능과 분리할 기능 : ...
    - 영향받을 것으로 예상 되는 외부 대상 :
        - `Asset/Script/XXX.cs`
        - `Asset/Script/XXX.cs`
        - `Asset/Prefab/XXX`
        - ...

    [클래스] 기능 분리에 승인하시겠습니까?
    ```

## 주석

- HLSL·셰이더 코드의 초보자용 상세 한국어 주석과 중복 설명 방지는 [HLSL 컨벤션](<../Graphics/HLSL Convention.md>)을 함께 따른다.
- 코드만으로 의도나 이유를 이해하기 어려운 부분에 주석을 단다. 주석을 달 때는 필요한 동작과 이유를 설명한다.
- 기본적으로 핵심을 간결하고 정확하게 적는다. 사용자가 자세한 주석을 요청하면 이해에 필요한 범위에서 상세하게 작성한다.
- 불필요한 내용이나 예시, 애매하거나 모호한 표현으로 적지 않고, 과장하거나 거짓된 내용, 임의로 가정하거나 추측한 내용을 적지 않는다.
- 주석으로 쓴 줄은 `기능 분리`에서 검토하는 길이에 해당하지 않는다.


## 자동 포맷
- 기존 .editorconfig와 포맷 도구를 먼저 확인한다. 이 문서와 충돌하면 프로젝트 기준을 확정한다.
- 도구 설치·저장소 전체 재정렬은 별도 변경으로 취급한다.
- 줄바꿈·인코딩은 기존 파일 기준을 유지하고 새 파일 기준은 프로젝트에서 확정한다.
- 명명·접근 제어·프로퍼티 형태는 기존 Code Convention.md에서 관리한다.
