import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {

  public eventos : any = [];
  widthImg = 80;
  heightImg = 80;
  marginImg = 2;



  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.getEventos();
  }

  showImg = true;
  changeImg(){
    this.showImg = !this.showImg; //vai receber o oposto dele (false/true)
  }

  public getEventos(): void { //getEvento é a conexão com o back
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => this.eventos = response,
      error => console.log(error)
      //complete
    );
    //está sendo realizado um get no protocolo http, dentro da url e está se inscrevendo que está retornando 3 itens

  }
}
