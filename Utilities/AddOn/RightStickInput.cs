using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Scripting;

#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
[Preserve]
[DisplayStringFormat("{horizontal}/{vertical}")]
public class StickComposite : InputBindingComposite<Vector2>
{

    [InputControl(layout = "Axis")]
    public int horizontal, vertical;

    public override Vector2 ReadValue(ref InputBindingCompositeContext context)
    {
        var horizontalAxis = context.ReadValue<float>(horizontal);
        var verticalAxis = context.ReadValue<float>(vertical);

        return new Vector2(horizontalAxis, verticalAxis);
    }

    public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
    {
        var value = ReadValue(ref context);
        return value.magnitude;
    }

#if UNITY_EDITOR
    static StickComposite()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Initialize()
    {
        InputSystem.RegisterBindingComposite<StickComposite>();
    }
}