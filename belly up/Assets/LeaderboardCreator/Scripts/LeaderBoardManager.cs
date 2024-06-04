using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using Dan.Models;
using TMPro;


public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField]Transform scrollViewContents;
    public List<GameObject> leaderboardObjects;
    [SerializeField]GameObject leaderBoardTemplateObject;
    [SerializeField]TextMeshProUGUI loadingText;
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TMP_InputField name;
    [SerializeField]GameObject highScoreDisplay;
    [SerializeField]GameObject submissionDisplay;

    Coroutine loadingAnimation;
    void Start()
    {
        GetEntryCount();
        loadingAnimation = StartCoroutine(loadingAnim());
    }

    void OnEnable()
    {
        if(Int32.Parse(scoreText.text) >= PlayerPrefs.GetInt("highScore"))
        {
            submissionDisplay.SetActive(true);
        }
        else
        {
            highScoreDisplay.SetActive(true);
        }
    }

    void GetEntryCount()
    {
        Leaderboards.belly_up.GetEntries(OnEntriesLoaded, ErrorCallback);
    }

    public void SubmitEntry()
    {
        string nameyName = name.text;
        if(string.IsNullOrEmpty(name.text))
        {
            nameyName = "fish #" + UnityEngine.Random.Range(0,999).ToString();
        }
        Leaderboards.belly_up.UploadNewEntry(nameyName, Int32.Parse(scoreText.text), ConfirmEntry);
        foreach(GameObject objective in leaderboardObjects)
        {
            Destroy(objective);
        }
        leaderboardObjects.Clear();
        loadingText.gameObject.SetActive(true);
    }

    void ConfirmEntry(bool hooray)
    {
        if(hooray)
        {
            GetEntryCount();
        }
    }

    void OnEntriesLoaded(Entry[] entries)
    {
        foreach(Entry entry in entries)
        {
            string username = entry.Username;
            string score = entry.Score.ToString();
            GameObject leaderCard = Instantiate(leaderBoardTemplateObject, transform.position, Quaternion.identity, scrollViewContents);
            LeaderboardObject leaderCardComponent = leaderCard.GetComponent<LeaderboardObject>();
            leaderCardComponent.nameAndScore.SetText(username + " - " + score + " P");
            var dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(entry.Date);
            string leadTime = $"{dateTime.Hour:00}:{dateTime.Minute:00}:{dateTime.Second:00} (UTC)";
            string leadDate = $"{dateTime:dd/MM/yyyy}";
            leaderCardComponent.dateAndTime.SetText(DateCompiler(leadDate.Split("/")) + " at " + leadTime);
            //i have autism
            leaderboardObjects.Add(leaderCard);
        }
        loadingText.gameObject.SetActive(false);
    }

    public DateTime UnixSecondsToDateTime(long timestamp, bool local = false)
    {
        var offset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
        return local ? offset.LocalDateTime : offset.UtcDateTime;
    }

    private void ErrorCallback(string error)
    {
        Debug.LogError(error);
    }

    IEnumerator loadingAnim()
    {
        loadingText.SetText("Loading");
        yield return new WaitForSeconds(0.1f);
        loadingText.SetText("Loading.");
        yield return new WaitForSeconds(0.1f);
        loadingText.SetText("Loading..");
        yield return new WaitForSeconds(0.1f);
        loadingText.SetText("Loading...");
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(loadingAnim());
    }

    string DateCompiler(string[] dateComponents)
    {
        string day = "7th";
        string month = "March";
        string year = dateComponents[2];
        switch(dateComponents[0])
            {
                case "01":
                day = "1st";
                break;

                case "02":
                day = "2nd";
                break;

                case "03":
                day = "3rd";
                break;

                case "04":
                day = "4th";
                break;

                case "05":
                day = "5th";
                break;

                case "06":
                day = "6th";
                break;

                case "07":
                day = "7th";
                break;

                case "08":
                day = "8th";
                break;

                case "09":
                day = "9th";
                break;

                case "10":
                day = "10th";
                break;

                case "11":
                day = "11th";
                break;

                case "12":
                day = "12th";
                break;

                case "13":
                day = "13th";
                break;

                case "14":
                day = "14th";
                break;

                case "15":
                day = "15th";
                break;

                case "16":
                day = "16th";
                break;

                case "17":
                day = "17th";
                break;

                case "18":
                day = "18th";
                break;

                case "19":
                day = "19th";
                break;

                case "20":
                day = "20th";
                break;

                case "21":
                day = "21st";
                break;

                case "22":
                day = "22nd";
                break;

                case "23":
                day = "23rd";
                break;

                case "24":
                day = "24th";
                break;

                case "25":
                day = "25th";
                break;

                case "26":
                day = "26th";
                break;

                case "27":
                day = "27th";
                break;

                case "28":
                day = "28th";
                break;

                case "29":
                day = "29th";
                break;

                case "30":
                day = "30th";
                break;

                case "31":
                day = "31st";
                break;
            }
        switch(dateComponents[1])
        {
            case "01":
            month = "January";
            break;

            case "02":
            month = "February";
            break;

            case "03":
            month = "March";
            break;
            
            case "04":
            month = "April";
            break;
            
            case "05":
            month = "May";
            break;

            case "06":
            month = "June";
            break;

            case "07":
            month = "July";
            break;

            case "08":
            month = "August";
            break;

            case "09":
            month = "September";
            break;

            case "10":
            month = "October";
            break;

            case "11":
            month = "November";
            break;

            case "12":
            month = "December";
            break;
        }
        return month + " " + day + ", " + year;
    }
}
