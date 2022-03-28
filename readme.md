<h1>XREAL 2기 dev 존-레몬 과제</h1>
<p>이름: 장영웅</p>

<h3>git 기록</h3>
<li>fork 후 git과의 연결 확인</li>
<li>튜토리얼 "The Environment"까지 완료</li>
<li>튜토리얼 "Audio"까지 완료</li>
<li>튜토리얼 "Build"까지 전부 완료</li>

</br>

<h3>비고</h3>
```
NullReferenceException: Object reference not set to an instance of an object
UnityEditor.Graphs.Edge.WakeUp () (at <b82d8d0a349d4d70807c2fc5746a710f>:0)
UnityEditor.Graphs.Graph.DoWakeUpEdges (System.Collections.Generic.List`1[T] inEdges, System.Collections.Generic.List`1[T] ok, System.Collections.Generic.List`1[T] error, System.Boolean inEdgesUsedToBeValid) (at <b82d8d0a349d4d70807c2fc5746a710f>:0)
UnityEditor.Graphs.Graph.WakeUpEdges (System.Boolean clearSlotEdges) (at <b82d8d0a349d4d70807c2fc5746a710f>:0)
UnityEditor.Graphs.Graph.WakeUp (System.Boolean force) (at <b82d8d0a349d4d70807c2fc5746a710f>:0)
UnityEditor.Graphs.Graph.WakeUp () (at <b82d8d0a349d4d70807c2fc5746a710f>:0)
UnityEditor.Graphs.Graph.OnEnable () (at <b82d8d0a349d4d70807c2fc5746a710f>:0)
```
<p>위와 같은 에러메시지가 발생하는데 실행하는데 큰 문제가 없다.</p>
<p>그래픽과 관련된 문제인 것 같으니 튜토리얼 자체의 에러가 아닐까 한다.</p>