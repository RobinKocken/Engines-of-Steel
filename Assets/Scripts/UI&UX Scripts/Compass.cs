using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject iconPrefab;
    public RawImage compassImage;
    public Transform player;
    public Transform comp;
    public float maxDistance;

    public List<QuestMarker> questMarkers = new List<QuestMarker>();
    public List<GameObject> questIcons;
    public List<Quest> quests;

    float compassUnit;

    void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360;
    }

    void Update()
    {
        comp.position = player.position;

        compassImage.uvRect = new Rect(comp.localEulerAngles.y / 360, 0, 1, 1);    

        for(int i = 0; i < questMarkers.Count; i++)
        {
            if(questMarkers[i] == null)
            {
                questMarkers.RemoveAt(i);

                quests[i].IsFinished();

                Destroy(questIcons[i]);
                questIcons.RemoveAt(i);
            }
        }

        foreach(QuestMarker marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);

            float dst = Vector2.Distance(new Vector2(comp.transform.position.x, comp.transform.position.z), marker.Position);
            float scale = 0;

            if(dst < maxDistance)
                scale = 1 - (dst / maxDistance);

            marker.image.rectTransform.localScale = Vector3.one * scale;
        }
    }

    public void AddQuestMarker(QuestMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        questIcons.Add(newMarker);

        questMarkers.Add(marker);
    }

    Vector2 GetPosOnCompass(QuestMarker marker)
    {
        Vector2 compPos = new Vector2(comp.transform.position.x, comp.transform.position.z);
        Vector2 compFwd = new Vector2(comp.transform.forward.x, comp.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.Position - compPos, compFwd);

        return new Vector2(compassUnit * angle, 0);
    }
}
