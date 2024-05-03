using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nest : MonoBehaviour
{
    public List<Job> collectJobs;
    public List<Job> buildJobs;
    public Slider collectSlider;
    public Slider buildSlider;
    public List<AntBehaviour> workers;
    public int workerAmount;

    void Start()
    {
        buildJobs = GameObject.FindObjectOfType<BuildGrid>().jobs;
        collectJobs = new List<Job>();
        collectSlider.maxValue = 100;
        buildSlider.maxValue = 100;
        collectSlider.value = 50;
        buildSlider.value = 50;

    }

    void Update()
    {
        List<Job> jobes = collectJobs;

        foreach (Job job in collectJobs)
        {
            if (job.target == null)
            {
                jobes.Remove(job);
            }
        }
        collectJobs = jobes;

        //deletar jobs mortos ou terminados
        List<AntBehaviour> tempWorkers = workers;
        
        foreach(AntBehaviour worker in workers)
        {
            if(worker.gameObject == null)
            {
                tempWorkers.Remove(worker);
            }
        }
        workers = tempWorkers;
        //deletar trabalhadores mortos
        

        workerAmount = workers.Count;
        if(collectSlider.value + buildSlider.value > 100)
        {
            collectSlider.value -= 1;
            buildSlider.value -= 1;
        }

      
    }
    public Job GetJob(string type)
    {
       List<Job> jobs = new List<Job>();
       

        if (type == null)
        {
            int buildnum = countWorkers(buildJobs);
            int collectnum = countWorkers(collectJobs);
            string jobtype = null;
            if(collectnum == 0)
            {
                jobtype = "collect";
            }
            else if(buildnum == 0)
            {
                jobtype = "build";
            }
            else if (buildnum / (buildnum + collectnum) < buildSlider.value/100)
            {
                jobtype = "build";
            }
            else if (collectnum / (buildnum + collectnum) < collectSlider.value / 100)
            {
                jobtype = "collect";
            }
            type = jobtype;
        }
        if (buildJobs.Count == 0)
        {
            type = "collect";
        }
        if (collectJobs.Count == 0)
        {
            if (type == "collect")
            {
                return null;
            }
            else
            {
                type = "build";
            }
        }

        







        if (type == "collect")
        {
            jobs = collectJobs;

        }
        if (type == "build")
        {
            jobs = buildJobs;

        }




        Job selectedJob  = null;
        float x = Mathf.Infinity;
        foreach (Job j in jobs)
        {

            if (j.workerAmount < x)
            {
                x = j.workerAmount;
                selectedJob = j;
            }

        }

        if (selectedJob == null)
        {
            return null;
        }
        if (type == "collect")
        {
            collectJobs[collectJobs.IndexOf(selectedJob)].workerAmount+=1;
        }
        else if(type == "build")
        {
            buildJobs[buildJobs.IndexOf(selectedJob)].workerAmount+=1;
        }

        return selectedJob;
    }

    int countWorkers(List<Job> jobs)
    {
        int count = 0;
        foreach(Job job in jobs)
        {
            count += job.workerAmount;
        }
        return count;
    }
}
