using System;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace UnityEngine.XR.Content.Interaction
{
    public class Bomba : XRBaseInteractable
    {
        [Serializable]
        public class ValueChangeEvent : UnityEvent<float> { }

        [SerializeField]
        Transform m_Palanca = null;

        [SerializeField]
        float m_InfladoSpeed = 1.0f;

        [SerializeField]
        float m_DesinfladoSpeed = 1.0f;

        [SerializeField]
        float m_MaxPresion = 100.0f;

        [SerializeField]
        UnityEvent m_OnInflar = new UnityEvent();

        [SerializeField]
        UnityEvent m_OnDesinflar = new UnityEvent();

        [SerializeField]
        float m_MaxAltura = 0f; // Altura máxima a la que la palanca puede subir

        [SerializeField]
        float m_MinAltura = 0.0f; // Altura mínima a la que la palanca puede bajar

        [SerializeField]
        float m_MaxBajada = -0.0005f; // Altura máxima a la que la palanca puede bajar

        [SerializeField]
        [Tooltip("Altura de la palanca")]
        float m_Value = 0.5f;

        float m_PresionActual = 0.0f;
        bool m_EstaInflando = false;
        bool m_PuedeSubir = true;
        bool m_PuedeBajar = true;

        IXRSelectInteractor m_Interactor;

        public Transform palanca
        {
            get => m_Palanca;
            set => m_Palanca = value;
        }

        public UnityEvent onInflar => m_OnInflar;
        public UnityEvent onDesinflar => m_OnDesinflar;

        public float Value
        {
            get => m_Value;
            set
            {
                m_Value = Mathf.Clamp(value, m_MinAltura, m_MaxAltura);
                ActualizarPosicionPalanca();
            }
        }

        void Start()
        {
            m_PresionActual = 0.0f;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            selectEntered.AddListener(StartInflar);
            selectExited.AddListener(StopInflar);
        }

        protected override void OnDisable()
        {
            selectEntered.RemoveListener(StartInflar);
            selectExited.RemoveListener(StopInflar);
            base.OnDisable();
        }

        void StartInflar(SelectEnterEventArgs args)
        {
            m_EstaInflando = true;
        }

        void StopInflar(SelectExitEventArgs args)
        {
            m_EstaInflando = false;
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                if (isSelected)
                {
                    ProcesarPresion();
                    ProcesarPalanca();
                }
            }
        }

        void ProcesarPresion()
        {
            float alturaPalanca = m_Palanca.localPosition.z;

            if (m_EstaInflando && alturaPalanca < m_MaxAltura && m_PuedeSubir)
            {

                Debug.Log("m_EstaInflando"+m_EstaInflando);
                Debug.Log("alturaPalanca"+alturaPalanca);
                Debug.Log("m_PuedeSubir"+ m_PuedeSubir);


                m_PresionActual += m_InfladoSpeed * Time.deltaTime;

                if (m_PresionActual > m_MaxPresion)
                {
                    m_PresionActual = m_MaxPresion;
                    m_PuedeSubir = false; // Impedir que la palanca suba más allá de la altura máxima
                }

                m_PuedeBajar = true; // Permitir que la palanca baje después de haber alcanzado la altura máxima de subida

                m_OnInflar.Invoke();
            }
            else if (!m_EstaInflando && alturaPalanca > m_MinAltura && m_PuedeBajar)
            {
                Debug.Log("m_EstaInflando"+ m_EstaInflando);
                Debug.Log("alturaPalanca"+alturaPalanca);
                Debug.Log("m_PuedeBajar"+ m_PuedeBajar);
                Debug.Log("m_MinAltura"+ m_MinAltura);

                m_PresionActual -= m_DesinfladoSpeed * Time.deltaTime;

                if (m_PresionActual < 0.0f)
                {
                    m_PresionActual = 0.0f;
                    m_PuedeBajar = false; // Impedir que la palanca baje más allá de la altura máxima de bajada
                }

                m_PuedeSubir = true; // Permitir que la palanca suba después de haber alcanzado la altura máxima de bajada

                m_OnDesinflar.Invoke();
            }
        }

        void ProcesarPalanca()
        {
            float alturaPalanca = m_Palanca.localPosition.z;

            // Ajustar la presión según la velocidad de movimiento de la palanca
            float velocidadPalanca = Mathf.Abs((m_Palanca.localPosition - m_Palanca.localPosition).z) / Time.deltaTime;
            float ajusteVelocidad = Mathf.Clamp01(velocidadPalanca / m_InfladoSpeed); // Puedes ajustar el factor de velocidad según tus necesidades

            m_PresionActual += ajusteVelocidad * Time.deltaTime;

            // Actualizar la posición de la palanca basada en la presión actual
            m_Value = Mathf.Lerp(m_MinAltura, m_MaxAltura, m_PresionActual / m_MaxPresion);
            m_Value = Mathf.Clamp(m_Value, m_MaxBajada, m_MaxAltura); // Asegurar que la palanca no se salga de los límites
            m_Palanca.localPosition = new Vector3(m_Palanca.localPosition.x, m_Palanca.localPosition.y, m_Value);
        }

        void ActualizarPosicionPalanca()
        {
            m_Value = Mathf.Clamp(m_Value, m_MaxBajada, m_MaxAltura);
            m_Palanca.localPosition = new Vector3(m_Palanca.localPosition.x, m_Palanca.localPosition.y, m_Value);
        }
    }
}

