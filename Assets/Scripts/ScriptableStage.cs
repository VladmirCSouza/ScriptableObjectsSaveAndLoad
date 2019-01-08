using UnityEngine;

[CreateAssetMenu(fileName = "Stage Matrix", menuName = "New Stage Matrix", order = 1)]
public class ScriptableStage : ScriptableObject
{
    public MatrixLayout stageMatrix = new MatrixLayout();

    public string Name { get => name; set => name = value; }

    public void SetMatrixValue(int col, int row, int value = 0)
    {
        Debug.Log(col + " " + row + " " + value);
        stageMatrix.rows[row].cols[col] = value;
    }
}
