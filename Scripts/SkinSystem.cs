using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    public static SkinSystem Instance;

    [System.Serializable]
    public class Skin
    {
        public string name;
        public GameObject modelPrefab;
        public Vector3 spawnPosition; 
        public Vector3 spawnRotation; 
       
    }

    public Skin[] skins;
    public Transform skinParent;

    private GameObject currentSkinModel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadSkin();
    }

    public void ChangeSkin(int skinIndex)
    {
        if (skinIndex < 0 || skinIndex >= skins.Length) return;

        if (currentSkinModel != null)
            Destroy(currentSkinModel);

        if (skins[skinIndex].modelPrefab != null && skinParent != null)
        {
            currentSkinModel = Instantiate(skins[skinIndex].modelPrefab, skinParent);

           
            currentSkinModel.transform.localPosition = skins[skinIndex].spawnPosition;
            currentSkinModel.transform.localEulerAngles = skins[skinIndex].spawnRotation;
            

            RemovePhysicsComponents(currentSkinModel);
        }

        PlayerPrefs.SetInt("SelectedSkin", skinIndex);
        PlayerPrefs.Save();

        Debug.Log($"Скин изменен: {skins[skinIndex].name} на позиции: {skins[skinIndex].spawnPosition}");
    }

    void RemovePhysicsComponents(GameObject model)
    {
        Collider collider = model.GetComponent<Collider>();
        if (collider != null) Destroy(collider);

        Rigidbody rb = model.GetComponent<Rigidbody>();
        if (rb != null) Destroy(rb);
    }

    void LoadSkin()
    {
        int savedSkin = PlayerPrefs.GetInt("SelectedSkin", 0);
        ChangeSkin(savedSkin);
    }
}