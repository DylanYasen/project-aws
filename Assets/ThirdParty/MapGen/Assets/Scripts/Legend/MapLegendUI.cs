using Map;
using UnityEngine;

public class MapLegendUI : MonoBehaviour
{
    public GameObject legendEntryPrefab;
    public MapConfig mapConfig; // @todo: being lazy for now, but should get rid of direct reference

    public void Start()
    {
        foreach (var nodeBlueprint in mapConfig.nodeBlueprints)
        {
            var legend = Instantiate(legendEntryPrefab, transform);
            var legendEntryUI = legend.GetComponent<MapLegendEntryUI>();

            legendEntryUI.icon.sprite = nodeBlueprint.sprite;
            legendEntryUI.label.text = Map.Map.NodeTypeNames[nodeBlueprint.nodeType];
        }
    }
}
