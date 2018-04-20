using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameIntroduction : MonoBehaviour
{
    public Text historyText;

    private bool skipPressed;

    private int textSpeed;



    private const float TYPE_INTERVAL = 0.05f;
    private const float NEW_LINE_INTERVAL = 0.5f;
    private const float NEW_SCREEN_INTERVAL = 1.5f;

    private const string history1_1 = "\t\tOnce upon a time in the Kingdom of Gillamor, an ancient evil was defeated by a very powerful wizard and sealed in the temple of Zydis.\n";
    private const string history1_2 = "\t\tFor 300 years, Gillamor remained a peaceful and properous Kingdom, until the evil awoke again, more powerful and fearful than never before.";
    private const string history2_1 = "\t\tTo fight against the awaken evil, King Humfrey summoned Melisant and Benedict, a wizard couple known for being powerful, fearless, and faithful servants of the Kingdom.\n";
    private const string history2_2 = "\t\tAlthough Melisant and Benedict just had four twins, they didn't think of disobeying their King's order, not even for a second.";
    private const string history3_1 = "\t\tKnowing the peril their children would be in if they failed to defeat the great evil, before leaving to their journey they left each one of their child with a different trustworth friend.\n";
    private const string history3_2 = "\t\tTheir daughter Grizzel was left with their master, the Wizard Vrarugast.\n";
    private const string history3_3 = "\t\tStevyn was left with the Cleric who married them, Iliphyra.\n";
    private const string history3_4 = "\t\tDestrian was left with Alard, Melisant's noble brother.\n";
    private const string history3_5 = "\t\tAnd Ruven was left with Benedict's parents Gerolt and Nance, a couple of peasant farmers.";
    private const string history4_1 = "\t\tEach children was raised without knowing the existence of their brothers and developed different abilities.\n";
    private const string history4_2 = "\t\tGrizzel was trained by Vrarugast and became a powerful Wizard.\n";
    private const string history4_3 = "\t\tStevyn was trained by Iliphyra and became a Cleric, with healing powers.\n";
    private const string history4_4 = "\t\tDestrian, who was raised by Alard, became a very respected Knight.\n";
    private const string history4_5 = "\t\tAnd Ruven, living with Benedict's poor parents became a skillful Thief, mastering the arts of the Bow and Dagger.\n";
    private const string history5_1 = "\t\tFearing they would never come back, before leaving Melisant and Benedict also bound their children with a spell, which would unite them once they completed the age of 21.\n";
    private const string history5_2 = "\t\tAnd this was their wisest decision, as 21 years passed and they never came back to their children. The spell now is active, and attracted by each other, the twins met on their parent's old house.\n";
    private const string history5_3 = "\t\tDiscovering what happened with their parents, they decided to leave together in a dangerous mission and put an end to the Great Evil of Zydis.\n";
    private const string history5_4 = "\t\tAnd so, their journey begins...";

    // Use this for initialization
    void Start ()
    {
        textSpeed = 1;
        StartCoroutine(TypeHistory());
	}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            textSpeed = 0;
            skipPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            textSpeed = 1;
            skipPressed = false;
        }
    }

    IEnumerator TypeHistory()
    {
        yield return TypePart01();
        yield return TypePart02();
        yield return TypePart03();
        yield return TypePart04();
        yield return TypePart05();
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator TypePart01()
    {
        historyText.text = "";
        yield return Type(history1_1, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history1_2, textSpeed);
        yield return new WaitForSeconds(NEW_SCREEN_INTERVAL);
        
    }

    IEnumerator TypePart02()
    {
        historyText.text = "";
        yield return Type(history2_1, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history2_2, textSpeed);
        yield return new WaitForSeconds(NEW_SCREEN_INTERVAL);
       
    }

    IEnumerator TypePart03()
    {
        historyText.text = "";
        yield return Type(history3_1, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history3_2, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history3_3, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history3_4, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history3_5, textSpeed);
        yield return new WaitForSeconds(NEW_SCREEN_INTERVAL);
        
    }

    IEnumerator TypePart04()
    {
        historyText.text = "";
        yield return Type(history4_1, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history4_2, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history4_3, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history4_4, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history4_5, textSpeed);
        yield return new WaitForSeconds(NEW_SCREEN_INTERVAL);
        
    }

    IEnumerator TypePart05()
    {
        historyText.text = "";
        yield return Type(history5_1, 0);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history5_2, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history5_3, textSpeed);
        yield return new WaitForSeconds(NEW_LINE_INTERVAL);
        yield return Type(history5_4, textSpeed);
        yield return new WaitForSeconds(NEW_SCREEN_INTERVAL);
        
    }

    IEnumerator Type(string historyPart, float slowingFactor)
    {
        for (int i = 0; i < historyPart.Length; i++)
        {
            if (skipPressed == true)
            {
                slowingFactor = 0;
            }
            else
            {
                slowingFactor = 1;
            }
            
            yield return new WaitForSeconds(TYPE_INTERVAL * slowingFactor);

                historyText.text += historyPart[i];
            
           
            
        }
    }
}
