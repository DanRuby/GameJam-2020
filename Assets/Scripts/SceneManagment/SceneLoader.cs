using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] AudioClip hurtSound;

    AudioSource audioSource;
    Scene scene;
    Animator animator;


    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
        animator.SetTrigger("StartTransition");
    }

    public void ReloadCurrentLevel()
    {
        audioSource.PlayOneShot(hurtSound);
        SceneManager.LoadScene(scene.buildIndex);
        animator.SetTrigger("StartTransition");
    }
}
