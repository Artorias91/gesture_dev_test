﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecognitionGestureFeed_Universal.Djestit
{
    public class StateSequence
    {
        //attributi
        public int capacity {set; get;}
        public List<Token> tokens = new List<Token>();
        public int index {set;get;}


        //metodi
        //nel JS si chiamava init ma credo si possa utilizzare come un costruttore
        public StateSequence(int capacity)
        {
            if(this.capacity == capacity) 
                this.capacity = capacity;
            else 
                capacity = 2;
	        this.index = -1;
        }

        public StateSequence()
        {
            // TODO: Complete member initialization
        }

        public void _push(Token token)
        {
            if (this.tokens.Count > this.capacity)
            {
                this.tokens.Add(token);
                this.index++;
            }
            else
            {
                /// Ora però crasha anche qui, sappiamo il perché (ovvero risulta che capacity sia uguale a zero)
                /// però non capiamo il perché sostanzialmente (cioè al momento della creazione capacity lo inizializza, però poi diventa zero e ora non
                /// troviamo più il bug. Appena lo risolviamo rifacciamo il commit.
                this.index = (this.index + 1) % this.capacity;
                this.tokens.Insert(index, token);
            }
        }

        public virtual void push(Token token)
        {
            this._push(token);
        }

        public Token get(int delay)
        { 
            int pos = Math.Abs(this.index - delay) % this.capacity;
            return this.tokens[pos];
        }
    }
}
