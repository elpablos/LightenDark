## Jak skript funguje ##
Postava chodí ve čtverci 3x3 pořád dokola a snaží se rozdělat oheň a tím si zvýšit skill Camping. Je nutné mít v inventáři dřevo (Wooden Log). 

## Použití ##
Může být použito kdekoliv, kde je možné rozdělan oheň. V jeskyních to myslím nejde. Doporučuji dělat v guard zone. Skript je nekonečný, je nutné jej ukončit ručně.
Každý pokus zlikviduje 1 dřevo, vbudoucnu bude zlikvidováno pouze při úspěšném rozdělání ohně. Také je zatím bug, kdy se dřevo neodečítá v inventáři když je použito (na serveru je ale odečteno).

## Známé chyby ##
Zatím využívá pouze jeden druh dřeva. Oheň se nedá rozdělat na políčku, kde už je rozdělán, ale to se asi moc nestane... proto se chodí dokolečka.

## ToDo ##
* Využívat různé druhy dřeva
* Zjistit jestli je délka rozdělávání ovlivněna např. brněním a poté snížit čekací konstantu mezi jednotlivými pokusy

## EDIT ##
* upraveno pro novější verzi aplikace (async verze)

## Kód ##

```
#!c#
using LightenDark.Api;
using LightenDark.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Web.Script.Serialization;
using LightenDark.Api.Args;

namespace GeminiTester.Scripts
{
    public class CampingScript : ScriptBase
    {
        private String[] movement = new String[]
        {
            "moveLeft()",
            "moveLeft()",
            "moveLeft()",
            "moveDown()",
            "moveDown()",
            "moveDown()",
            "moveRight()",
            "moveRight()",
            "moveRight()",
            "moveUp()",
            "moveUp()",
            "moveUp()"
        };

        public override string DisplayName
        {
            get { return "CampingScript v2.0"; }
        }

        protected override async Task Run()
        {
            int i = 0;

            while (run)
            {
                // pohyb
                LogMessage("Starting setting fire " + i);
                SendJavascript(movement[i]);

                // tezba
                SendJavascript("ws.send('{\"type\":76,\"itemCode\":30020}');");

                await Task.Delay(16000);
                i++;

                if (i == movement.Length) 
                {
                    i = 0;
                }
            }
        }
    }
}
```