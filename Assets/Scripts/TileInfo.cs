using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo
{
    public Texture texture { get; private set; }
    public string meaning { get; private set; }

    public TileInfo(Texture texture,string meaning)
    {
        this.texture = texture;
        this.meaning = meaning;
    }
}
