DONE 
1. fai le scale DONE

4. fai altri livelli e vedi come passare da un livello all'altro (cerca di capire come usare un file esterno
   per sapere a che livello siamo) DONE
DOC -> adesso noi salviamo nel player ref l'ultimo livello giocato dal giocatore, una volta fatto ciò noi possiamo fare continue per continuare esattamente quel livello, oppure se ne scegliamo uno manualmente, verrà settato quello come nuovo ultimo livello giocato.
una volta sconfitto il livello si continua a quello successivo

5. fai che il player muore se viene colpito due volte DONE

6. added projectile anim DONE

7. fai le animazioni - player DONE

7. da mettere a posto lo shooting player script o projectile script, quando passiamo da heavymachinegun a hook, se abbiamo lasciato 3 proiettili
   vagare nel mondo, allora ognuno di questi oggetti metterà la sizeammo +1 perchè raggiungeremo 0 ma ognuno si aggiungerà, quindi possiamo tenere 3,4 proiettili in scena contemporaneamente lo stesso.
   vedere come aggiusralo -> aggiustato mettendo nel SetCreator del proiettile un informazione aggiuntiva del, che tipo di sparo mi ha creato?
   se mi ha creato un tipo diverso da quello attuale allora non ricarico le munizioni

3. rifai il movimento del player perchè fa cacare, in più aggiungici lo state pattern che determina se (idle,moving,climbing,falling) e collega queste info all'animator

TODO
8. manca per le palle e per i power up
   !! implementa **object pooling** per palle e ~~proiettili~~, così da rendere gestibile e scalabile un endless mode in cui
   instanzi prefab di platform e palle position per provare a fare il record.

2. fai UI (manca gestione menu opzioni e il menu di pausa nel gioco e UI in Game coi controlli fatti a bottone) 
rifai ui opzioni con le percentuali del main menu

6. setta la dimensione dello schermo alla safe area per la UI e inserisci bande nere per chi  ha uno schermo più grande del 18:9 1920x1080


!!riprendi da qui
7. ho aggiunto i power up, le armi funzionano e la capacità di sparare diversamente è all'interno dello script dello shoot del player.
il power up bomba funziona
il power up clock funziona ma devo mettere un sistema che mi esegue gli audio per ogni pickup  prima che vengano distrutti. (zawarudo)
la frutta funziona ma bisogna vedere come gestire la UI in base al punteggio che è tenuto dentro il manager.
capire come diminuire la responsabilità del manager rispetto a tutto il gioco e fare un po' di decoupling.



