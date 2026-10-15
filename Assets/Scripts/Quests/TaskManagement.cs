using Unity.VisualScripting;
using UnityEngine;

public class TaskManagement : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text questTask;
    [SerializeField] private TMPro.TMP_Text questName;
    [SerializeField] private TaskCheckerSO taskCheckerSO;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private AnimalEntity animalEntity;
    [SerializeField] private DialogueAnimator dialogueAnimator;

    private void Start()
    {
        if (enemyEntity != null)
            enemyEntity.OnDeath += SkeletonIsDie;
        if (animalEntity != null)
            animalEntity.OnAnimalDeath += CapybaraIsDie;
    }

    private void Update()
    {
        if (taskCheckerSO.tasksCompletedCount > 6 && dialogueAnimator != null)
        {
            dialogueAnimator.canTalk = true;
        }
    }

    public void FlowerIsPicked()
    {
        if (questName.text == "Ангел Хранитель")
        {
            if (taskCheckerSO.flowerCount == 0)
            {
                taskCheckerSO.flowerCount++;
                questTask.text = "Найти цветочки: 1/3";
            }
            if (taskCheckerSO.flowerCount == 1)
            {
                taskCheckerSO.flowerCount++;
                questTask.text = "Найти цветочки: 2/3";
            }
            if (taskCheckerSO.flowerCount == 2)
            {
                taskCheckerSO.flowerCount++;
                questTask.text = "Найти цветочки: 3/3";
                taskCheckerSO.questCompleted = true;
                TaskIsCompleted();
            }
        }
    }
    public void FlowerIsDropped()
    {
        if (taskCheckerSO.flowerCount == 3)
        {
            taskCheckerSO.flowerCount--;
            questTask.text = "Найти цветочки: 2/3";
        }
        if (taskCheckerSO.flowerCount == 2)
        {
            taskCheckerSO.flowerCount--;
            questTask.text = "Найти цветочки: 1/3";
        }
        if (taskCheckerSO.flowerCount == 1)
        {
            taskCheckerSO.flowerCount--;
            questTask.text = "Найти цветочки: 0/3";
        }
    }

    public void MushroomIsPicked()
    {
        if (questName.text == "Пивной Бизнес") 
        {
            if (taskCheckerSO.mushroomCount == 0)
            {
                taskCheckerSO.mushroomCount++;
                questTask.text = "Найти грибочки: 1/2";
            }
            if (taskCheckerSO.mushroomCount == 1)
            {
                taskCheckerSO.mushroomCount++;
                questTask.text = "Найти грибочки: 2/2";
                taskCheckerSO.questCompleted = true;
                TaskIsCompleted();
            }
        }
    }
    public void MushroomIsDropped()
    {
        if (taskCheckerSO.mushroomCount == 2)
        {
            taskCheckerSO.mushroomCount--;
            questTask.text = "Найти грибочки: 1/2";
        }
        if (taskCheckerSO.mushroomCount == 1)
        {
            taskCheckerSO.mushroomCount--;
            questTask.text = "Найти грибочки: 0/2";
        }
    }

    public void BushIsDie()
    {
        if (questName.text == "Колдун Телепат")
        {
            taskCheckerSO.bushDeathCount++;
            if (taskCheckerSO.bushDeathCount == 0)
                questTask.text = "Срубить кусты: 1/2";
            if (taskCheckerSO.bushDeathCount == 1)
            {
                questTask.text = "Срубить кусты: 2/2";
                taskCheckerSO.questCompleted = true;
                TaskIsCompleted();
            }
        }
    }

    public void SisterIsFound()
    {
        if (questName.text == "Воссоединение семьи")
        {
            taskCheckerSO.sisterIsFoundCount++;
            questTask.text = "Найти пропавшую сестру: 1/1";
            taskCheckerSO.questCompleted = true;

            TaskIsCompleted();
        }
    }

    private void CapybaraIsDie(object sender, System.EventArgs e)
    {
        if (questName.text == "Трагедия Семьи")
        {
            taskCheckerSO.capybaraCount++;
            questTask.text = "Убить капибару: 1/1";
            taskCheckerSO.questCompleted = true;
            TaskIsCompleted();
        }
    }
    private void SkeletonIsDie(object sender, System.EventArgs e)
    {
        if (questName.text == "Развитие классицизма")
        {
            taskCheckerSO.skeletonCount++;
            questTask.text = "Убить скелета: 1/1";
            taskCheckerSO.questCompleted = true;
            TaskIsCompleted();
        }
    }

    private void TaskIsCompleted()
    {
        taskCheckerSO.tasksCompletedCount++;
    }

}
