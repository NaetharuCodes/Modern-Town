using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    public static PlotManager Instance { get; private set; }

    private List<Plot> _plots = new();

    void Awake()
    {
        Instance = this;
        _plots.AddRange(FindObjectsByType<Plot>(FindObjectsSortMode.None));
    }

    public Plot GetPlotAt(Vector3Int tile) => _plots.Find(p => p.ContainsTile(tile));

    public Plot GetFamilyPlot(Family family) => _plots.Find(p => p.owner == family);

    public List<Plot> GetPlotsForSale() => _plots.FindAll(p => p.forSale && !p.IsOwned);
}
