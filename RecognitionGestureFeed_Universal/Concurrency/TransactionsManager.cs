﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
// Debug
using System.Diagnostics;
// Gestione Transazioni
using System.Transactions;
// Modifies
using RecognitionGestureFeed_Universal.Feed.FeedBack.Tree.Wrapper.CustomAttributes;
// Handler
using RecognitionGestureFeed_Universal.Feed.FeedBack.Tree.Wrapper.Handler;

namespace RecognitionGestureFeed_Universal.Concurrency
{
    /// <summary>
    /// Attualmente avremo a che fare solo con transazioni locali (distribuite in futuro?). Per far ciò abbiamo bisogno
    /// di: 1) Local Transaction Manager (che si occupa di mantenere un log, da utilizzare in caso di errore), e che
    /// coordina le transazioni su singole risorse; 2) Transaction Coordinator, che si occupa invece di far partire
    /// l'esecuzione delle transazioni, e il risultato finale sarà il commit o il rollback delle operazioni. 
    /// 
    /// Classe che si occupa di gestire la concorrenza tra più transazioni. Quando più transazioni operano su oggetti
    /// diversi, queste vengono eseguite senza problemi. Viceversa entra in gioco il concetto di gestione della 
    /// concorrenza, ottenuta utilizzando gli elementi messi a disposizione dalla classe System.Transactions.
    /// Quando una gesture viene eseguita correttamente, la sua funzione viene mandata alla TransactionsManager
    /// che si occuperà di verificare la presenza di elementi in conflitto e della loro esecuzione.
    /// </summary>
    public class TransactionsManager 
    {
        // Indica se è già in atto una transazione
        private bool isDoTransactions;

        /* Attributi */
        // Lista di tutti i modifies della classe
        protected List<Modifies> listModifies = new List<Modifies>();

        protected void OnTransactionExcute(Handler handler, List<Modifies> modConfl, List<Modifies> modNoConfl)
        {
            // Esecuzione cambiamenti
            using (TransactionScope scope = new TransactionScope())
            {
                // Esegue le modifiche non in conflitto, se presenti
                /*using (TransactionScope scopeNoConfl = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    this.execChange(modNoConfl, this.listModifies);
                    // Transazione Completata
                    scopeNoConfl.Complete();
                }*/

                // Esegue i cambiamenti in conflitto
                //this.execChange(modConfl, this.listModifies);

                // Transazione Completata
                //scope.Complete();
            }

            //Transaction c = new Transaction();
            

            
            /*
            // Se non c'è uno scope attivo, allora lo sia avvia
            if (numTransactionInExecution == 0)
            {
                numTransactionInExecution++;// Inserire codice per atomicità
                startTransaction();
            }
            else if(numTransactionInExecution < numTransactionManageable)
            {
                // Creo una transazione e cerco di inserirla nello scope previsto
            }
            else
            {
                // Attendi 
            }*/

            // Se non ci sono già altre transazioni in esecuzione allora faccio partire lo scope, in modo da poterle 
            // eseguire in sicurezza. Altrimenti creo una transazione e la invio allo scope.
            /*if (!isDoTransactions)
            {
                isDoTransactions = true;
                startTransaction(handler, mod);
            }
            else
            {    
                // Crea transazione
                //_enlistedTransaction = Transaction.Current;
                _enlistedTransaction.EnlistVolatile(this,  EnlistmentOptions.None);
                // Funzione che si occupa di liberare le risorse una volta che la transazione è stata completata
                this._enlistedTransaction.TransactionCompleted += this.onTransactionCompletedHandler;
            }*/
        }

        // Esecuzione 
        private void execChange(List<Modifies> listMod, List<Modifies> listElem)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Cicla la lista di modifies della classe principale, e li confronta con quelli
                // passati in input
                foreach (var e in listMod)
                {
                    // Ricerca la corrispondenza
                    Modifies c = listElem.Find(item => item.name == e.name);//item.Equals(e));
                    if (c != null)
                    {
                        // Esegue la modifica
                        c.setAttr(e.newv);
                    }
                    else
                    {
                        // Se arriva a questo punto vuol dire che c'è stato un errore, lancia 
                        // un'eccezione, indicando qual'è il modifies che non è stato trovato.
                        throw new InvalidModifiesException("Modifies non trovato! Errore!", e);
                    }
                }

                scope.Complete();
            }
        }
    }
}
