import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-criptografia'
})
export class CriptografiaComponent implements OnInit {

  constructor() { }

	

  ngOnInit(): void {
  }

  public criptografar(objeto : any){
     var LoginCaracteresIniciais = this.switchString(objeto.login.toUpperCase().split(""));
     var SenhaCaracteresIniciais = this.switchString(objeto.senha.toUpperCase().split(""));
  }

  private switchString(objeto : any){  

    var stringConcatenada = "";

    objeto.forEach(function (value) {      
      switch (value) {
        case 'A':
        stringConcatenada += "01010";
        break;
        case 'B':
        stringConcatenada += "01010";
        break;
        case 'C':
        stringConcatenada += "10100";
        break;
        case 'D':
        stringConcatenada += "00000";
        break;
        case 'E':
        stringConcatenada += "00001";
        break;
        case 'F':
        stringConcatenada += "10001";
        break;
        case 'G':
        stringConcatenada += "01000";
        break;
        case 'H':
        stringConcatenada += "01001";
        break;
        case 'I':
        stringConcatenada += "00000";
        break;
        case 'J':
        stringConcatenada += "01111";
        break;
        case 'K':
        stringConcatenada += "00011";
        break;
        case 'L':
        stringConcatenada += "10000";
        break;
        case 'M':
        stringConcatenada += "00100";
        break;
        case 'N':
        stringConcatenada += "01000";
        break;
        case 'O':
        stringConcatenada += "00010";
        break;
        case 'P':
        stringConcatenada += "01011";
        break;
        case 'Q':
        stringConcatenada += "11110";
        break;
        case 'R':
        stringConcatenada += "01110";
        break;
        case 'S':
        stringConcatenada += "00111";
        break;
        case 'T':
        stringConcatenada += "00110";
        break;
        case 'U':
        stringConcatenada += "01100";
        break;
        case 'V':
        stringConcatenada += "11000";
        break;
        case 'W':
        stringConcatenada += "11111";
        break;
        case 'X':
        stringConcatenada += "10111";
        break;
        case 'Y':
        stringConcatenada += "11011";
        break;
        case 'Z':
        stringConcatenada += "11110";
        break;
        case '1':
        stringConcatenada += "99/**";
        break;
        case '2':
        stringConcatenada += "*/*99";
        break;
        case '3':
        stringConcatenada += "7.;{}";
        break;
        case '4':
        stringConcatenada += "88.8.";
        break;
        case '5':
        stringConcatenada += "..|..";
        break;
        case '6':
        stringConcatenada += "||.||";
        break;
        case '7':
        stringConcatenada += "[-96/";
        break;
        case '8':
        stringConcatenada += "9/*99";
        break;
        case '9':
        stringConcatenada += "@lunn";
        break;
        case '0':
        stringConcatenada += "llunn";
        break;
      }
      
      return stringConcatenada;
    });

  }

}