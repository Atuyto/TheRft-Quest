using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemMessage : MonoBehaviour
{
    private string titre;
    private string code;
    
    public void Initialize(string titre, string code)
    {
        this.titre = titre;
        this.code = code;
    }

    public string GetCode()
    {
        return code;
    }

    public void SetCode(string code)
    {
        this.code = code;
    }

    public string GetTitre()
    {
        return titre;
    }

    public void SetTitre(string titre)
    {
        this.titre = titre;
    }
}
