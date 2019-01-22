using UnityEngine;

[CreateAssetMenu(fileName = "Stage Matrix", menuName = "New Stage Matrix", order = 1)]
public class ScriptableStage : ScriptableObject
{
    public MatrixLayout stageMatrix = new MatrixLayout();

    public string Name { get => name; set => name = value; }

    public void SetDimension(int width, int height) => stageMatrix.SetDimension(width, height);
  
    public void SetMatrixValue(int col, int row, int value = 0)
    {
        if (stageMatrix.cols == null)
            stageMatrix = new MatrixLayout();
        if (stageMatrix.cols[row] == null)
            stageMatrix.cols[row] = new Col();
        stageMatrix.cols[col].rows[row] = value;
    }
}
