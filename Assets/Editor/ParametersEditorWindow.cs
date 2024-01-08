using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParametersEditorWindow : EditorWindow
{
    private PredestrianParameters predestrianParameters;
    private S1CarControllerParameters s1ParametersDeplacementVoiture;
    private S1ParameterPredestrian s1ParameterPredestrian;
    private S2ParametersDeplacementVoiture s2ParametersDeplacementVoiture;
    private GameObject selectedObject;
    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 newScale;

    private GUIStyle objectNameStyle;
    [MenuItem("Outil de Parametrage/Custom Object Window %&t")]
    public static void ShowWindow()
    {
        ParametersEditorWindow window = GetWindow<ParametersEditorWindow>("Object Window");
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;

        // Créez le style pour le nom de l'objet
        objectNameStyle = new GUIStyle(EditorStyles.label);
        objectNameStyle.fontStyle = FontStyle.Bold;
        objectNameStyle.fontSize = 16; // Taille de police plus grande
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }


    private void OnGUI()
    {
        if (selectedObject != null)
        {
            GUILayout.Label("Selected Object:", objectNameStyle); // Utilisez le style personnalisé
            GUILayout.Label(selectedObject.name, objectNameStyle); // Utilisez le style personnalisé pour le nom de l'objet
            GUILayout.Space(10);

            GUILayout.Label("Object Transform", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            // Affichez les champs de transformation et sauvegardez les nouvelles valeurs
            newPosition = EditorGUILayout.Vector3Field("Position", newPosition);
            newRotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", newRotation.eulerAngles));
            newScale = EditorGUILayout.Vector3Field("Scale", newScale);

            // Si les valeurs ont changé, mettez à jour la transformation de l'objet
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedObject.transform, "Modify Transform");

                selectedObject.transform.position = newPosition;
                selectedObject.transform.rotation = newRotation;
                selectedObject.transform.localScale = newScale;
            }
            // Vérifiez le type de l'objet sélectionné et affichez les propriétés appropriées
            if (selectedObject.GetComponent<DeplacementPieton>() != null)
            {
                predestrianParameters = selectedObject.GetComponent<DeplacementPieton>().properties;
                GUILayout.Label("Parametre pieton", EditorStyles.boldLabel);
                predestrianParameters.vitesseMax = EditorGUILayout.FloatField("Max Speed", predestrianParameters.vitesseMax);
                
            }
            // la voiture de la scene 2
            else if (selectedObject.GetComponent<S2DeplacementVoiture>() != null)
            {
                s2ParametersDeplacementVoiture = selectedObject.GetComponent<S2DeplacementVoiture>().properties;
                GUILayout.Label("Parametres Voiture", EditorStyles.boldLabel);
                s2ParametersDeplacementVoiture.vitesseKMH = EditorGUILayout.FloatField("Max Speed", s2ParametersDeplacementVoiture.vitesseKMH);
            }

            // la voiture de scene 1
            else if (selectedObject.GetComponent<CarController>() != null)
            {
                s1ParametersDeplacementVoiture = selectedObject.GetComponent<CarController>().properties;
                GUILayout.Label("Parametres Voiture", EditorStyles.boldLabel);
                s1ParametersDeplacementVoiture.motorForce = EditorGUILayout.FloatField("Force Moteur", s1ParametersDeplacementVoiture.motorForce);
                GUILayout.Label("Parametres Voiture", EditorStyles.boldLabel);
                s1ParametersDeplacementVoiture.brakeForce = EditorGUILayout.FloatField("force arret", s1ParametersDeplacementVoiture.brakeForce);
                 GUILayout.Label("Parametres Voiture", EditorStyles.boldLabel);
                s1ParametersDeplacementVoiture.distanceArret = EditorGUILayout.FloatField("distance arret", s1ParametersDeplacementVoiture.distanceArret);

                // Début de la disposition horizontale
                GUILayout.Label("ACCELERATION Voiture", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();

                // Champ d'entrée pour la vitesse de départ initiale
                s1ParametersDeplacementVoiture.vitesseDepart = EditorGUILayout.FloatField("Vitesse progressive", s1ParametersDeplacementVoiture.vitesseDepart);

                // Champ d'entrée pour la vitesse à atteindre
                s1ParametersDeplacementVoiture.vitesseAAtteindre = EditorGUILayout.FloatField("Vitesse à atteindre", s1ParametersDeplacementVoiture.vitesseAAtteindre);

                // Fin de la disposition horizontale
                GUILayout.EndHorizontal();
            }

            else if (selectedObject.GetComponent<PedestrianController>() != null)
            {
                s1ParameterPredestrian = selectedObject.GetComponent<PedestrianController>().properties;
                GUILayout.Label("Parametre Pieton", EditorStyles.boldLabel);
                s1ParameterPredestrian.walkingSpeed = EditorGUILayout.FloatField("Max Speed", s1ParameterPredestrian.walkingSpeed);
                GUILayout.Label("Trigger", EditorStyles.boldLabel);
                s1ParameterPredestrian.DistanceDecision = EditorGUILayout.FloatField("distance de decision", s1ParameterPredestrian.DistanceDecision);

            }


        }

        else
        {
            GUILayout.Label("No object selected.", EditorStyles.boldLabel);
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (selectedObject != null)
        {
            Vector3 objectScreenPosition = Camera.current.WorldToScreenPoint(selectedObject.transform.position);
            objectScreenPosition.y = Screen.height - objectScreenPosition.y;

            Vector2 editorScreenPosition = new Vector2(objectScreenPosition.x - 150, objectScreenPosition.y - position.height);
            this.position = new Rect(editorScreenPosition, position.size);
        }

       
    }

    private void Update()
    {
        if (Selection.activeGameObject != selectedObject)
        {
            selectedObject = Selection.activeGameObject;
            if (selectedObject != null)
            {
                newPosition = selectedObject.transform.position;
                newRotation = selectedObject.transform.rotation;
                newScale = selectedObject.transform.localScale;
            }

            Repaint();
        }
    }
}
