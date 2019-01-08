[System.Serializable]
public class Col
{
    public int[] cols = new int[13];
}

[System.Serializable]
public class MatrixLayout
{
    public Col[] rows = new Col[13];
}
