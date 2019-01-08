using UnityEngine;

public class ScriptableStageReadTest : MonoBehaviour {

    private int[,] stageValues = new int[13,13];

    public ScriptableStage stageData;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                //stageValues[i, j] = stageData.GetCellValue(i, j);
                //Debug.Log("[" + i + "," + j + "] = " + stageValues[i, j]);
            }

        }

        //Debug.Log(stageData.GetStageName());
	}
}
