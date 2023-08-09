using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetDialogueText : MonoBehaviour
{
    public FalasNPCDaLoja falas;
    public TextMeshProUGUI textoFalas;

    public void ShowLines()
    {
        var Dialogos = falas.falas[Random.Range(0, falas.falas.Length)];
        textoFalas.text = Dialogos;
    }

}
