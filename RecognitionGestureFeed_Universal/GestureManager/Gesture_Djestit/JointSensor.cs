﻿using RecognitionGestureFeed_Universal.Djestit;
using RecognitionGestureFeed_Universal.Recognition.BodyStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Kinect
using Microsoft.Kinect;

namespace RecognitionGestureFeed_Universal.GestureManager.Gesture_Djestit
{
    public class JointSensor
    {
        //public event GestureEventArgs onSkeletonStart;
        //public event GestureEventArgs onSkeletonMove;
        //public event GestureEventArgs onSkeletonEnd;
        //element... cosa èhhh?? Dovrebbe essere un acquisition manager
        private int capacity;
        private Term root;
        public JointStateSequence sequence;
        private List<int> gestureToEvent;
        private List<int> eventToGesture;

        public JointSensor(Term root, int capacity)
        {
            this.capacity = capacity;
            this.root = root;
            //statesequence e gesturestatesequence hanno il tipo di capacity diverso. controllare!!
            this.sequence = new JointStateSequence(this.capacity);
            this.eventToGesture = new List<int>();
            this.gestureToEvent = new List<int>();
            this.gestureToEvent.Add(-1);
        }

       /* public Token generateToken(TypeToken type, JointInformation jointInformation){
            JointToken token = new JointToken(type, jointInformation);
            switch(type)
            {
                case TypeToken.Start:
                    var touchId = this.firstId(jointInformation.id);
                    int identifier = firstId(jointInformation.idSkeleton);
                    this.eventToGesture[jointInformation.idSkeleton] = identifier;
                    this.gestureToEvent[identifier] = jointInformation.idSkeleton;
                    token.identifier = (ulong)identifier; 
                    break;
                case TypeToken.Move:
                    token.identifier = (ulong)this.eventToGesture[jointInformation.idSkeleton];
                    break;
                case TypeToken.End:
                    token.identifier = (ulong)this.eventToGesture[jointInformation.idSkeleton];
                    this.eventToGesture.RemoveAt(jointInformation.idSkeleton);
                    this.gestureToEvent.RemoveAt((int)token.identifier);
                    break;
                default:
                    break;           
            }
            this.sequence.push(token);
            //token.sequence = this.sequence; ???? MIO DIO!!!!
            return token;
        }*/
        public NewJointToken generateToken(TypeToken typeToken, NewJointToken jointToken)
        {
            int ID = jointToken.identifier;
            NewJointToken token = new NewJointToken(typeToken, jointToken.jointType, jointToken.x, jointToken.y, jointToken.z, ID);
            switch (typeToken)
            {
                case TypeToken.Start:
                    int touchId = this.firstId(jointToken.identifier);
                    this.eventToGesture.Add(touchId);// Perché non insert(jointToken.identifier, touchId);?
                    this.gestureToEvent[touchId] = jointToken.identifier;
                    token.identifier = touchId;
                    break;
                case TypeToken.Move:
                    token.identifier = this.eventToGesture[jointToken.identifier];
                    break;
                case TypeToken.End:
                    token.identifier = this.eventToGesture[jointToken.identifier];
                    this.eventToGesture.RemoveAt(ID);
                    this.gestureToEvent.RemoveAt((int)token.identifier);
                    break;
                default:
                    break;
            }
            this.sequence.push(token);
            //token.sequence = this.sequence; ???? MIO DIO!!!!
            return token;
        }

        public int firstId(int id)
        {
            for (var i = 1; i < this.gestureToEvent.Count(); i++)
            {
                if (gestureToEvent[i] == null)
                    return i;
            }
            this.gestureToEvent.Add(id);
            return this.gestureToEvent.Count() - 1;
        }

        /*public void raiseMoveEvent(Skeleton skeleton, TypeToken typeToken)
        {
            foreach(JointInformation ji in skeleton.getListJointInformation())
            {
                Token token = this.generateToken(typeToken, ji);
                this.root.fire(token);
            }
        }*/

        /*
        public void _onSkeletonStart(Skeleton sender)
        {
            this.raiseMoveEvent(sender, TypeToken.Start);
        }
        public void _onSkeletonMove(Skeleton sender)
        {
            this.raiseMoveEvent(sender, TypeToken.Move);
        }
        public void _onSkeletonEnd(Skeleton sender)
        {
            this.raiseMoveEvent(sender, TypeToken.End);
        }*/
    }
}
