﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Leap
using Leap;

namespace RecognitionGestureFeed_Universal.Recognition.FrameDataManager
{
    public class LeapData : ICloneable
    {
        /* Attributi */
        // Lista delle Arm rilevate dalla Leap Motion
        public List<Arm> armList { get; private set; }
        // Lista che contiene gli Hands rilevati
        public HandList handList { get; private set; }
        // Lista che contiene i Fingers rilevati
        public FingerList fingerList { get; private set; }
        // Lista dei Pointables rilevate
        public ToolList pointableList { get; private set; }
        // Lista delle immagini
        public ImageList imageList { get; private set; }
        // Lista dell'InteractionBox contenuto nel frame ricevuto dal Leap Motion
        public InteractionBox interactionBox { get; private set; }
        // Descrizione in byte del contenuto del frame
        public byte[] frameByte { get; private set; }
        // Timestamp del frame
        public float timestamp { get; private set; }

        /* Costruttore */
        public LeapData()
        {
            // Inizializzo le diverse liste
            this.armList = new List<Arm>();
            this.handList = new HandList();
            this.fingerList = new FingerList();
            this.pointableList = new ToolList();
            this.imageList = new ImageList();
            this.interactionBox = new InteractionBox();
        }

        /* Metodi */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        public void update(Frame frame)
        {
            // Si vanno a leggere tutte le informazioni contuenute nel frame, e si aggiornano le
            // struttere dati in esso presenti
            // Aggiorna la lista delle hands
            this.handList = frame.Hands;
            // Aggiorna la lista dei fingers
            this.fingerList = frame.Fingers;
            // Aggiorna la lista degli Tools
            this.pointableList = frame.Tools;
            // Recupera le ultime immagini rilevate dal Leap
            this.imageList = frame.Images;
            // Aggiorna l'InteractionBox
            this.interactionBox = frame.InteractionBox;
            // Aggiorna il timestamp dell'ultimo frame
            this.timestamp = frame.Timestamp;
            // Aggiorna la lista delle arms
            this.armList.Clear();
            foreach(Hand hand in this.handList)
            {
                if (hand.Arm.IsValid)
                    armList.Add(hand.Arm);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}