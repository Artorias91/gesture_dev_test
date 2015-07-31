﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Diagnostic
using System.Diagnostics;
// Writable
using System.Windows.Media;
using System.Windows.Media.Imaging;
// Leap
using Leap;
// Stream
using RecognitionGestureFeed_Universal.Recognition.Leap.Stream;
// LeapData
using RecognitionGestureFeed_Universal.Recognition.FrameDataManager;

namespace RecognitionGestureFeed_Universal.Recognition.Leap
{
    // Delegate dell'evento BitmapUpdate
    public delegate void BitmapUpdate(ImageSource bitmap);

    public class StreamManager
    {
        /* Eventi */
        // Evento che viene lanciato per comunicare l'arrivo di una nuova immagine
        public static event BitmapUpdate OnImageUpdate;

        /* Attributi */
        // Writable che rappresenta l'immagine acquisita dal leap
        private static WriteableBitmap imageBitmap = null;

        /* Costruttore */

        /* Metodi */
        /// <summary>
        /// Inizializza la visualizzazione
        /// </summary>
        /// <param name="managerLeap"></param>
        public static void StartImageStream(AcquisitionManagerLeap managerLeap)
        {
            managerLeap._OnFrame += updateImageStream;
        }

        /// <summary>
        /// Aggiorna lo Stream
        /// </summary>
        /// <param name="leapData"></param>
        private static void updateImageStream(FrameDataManager.LeapData leapData)
        {
            Debug.WriteLine(leapData.image.Width + " - " + leapData.image.Height);
            Debug.WriteLine("Porcamadonna: " + leapData.handList[0].IsValid);
            // Inizializza il WriteableBitmap
            /*if (imageBitmap == null)
            {
                //imageBitmap = new WriteableBitmap(leapData.imageList[0].Width, leapData.imageList[0].Height, 96.0, 96.0, PixelFormats.Gray32Float, null);
            }
            // Aggiorna l'immagine
            imageBitmap.convertBitmap(leapData.imageList[0]);
            _OnImageUpdate();*/
        }

        /// <summary>
        /// Lancia l'evento OnImageUpdate
        /// </summary>
        public static void _OnImageUpdate()
        {
            if (OnImageUpdate != null)
                OnImageUpdate(imageBitmap);
        }
    }
}
