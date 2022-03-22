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
  private _filterList = ''
  public eventosFiltrados : any = []

  public get filterList(){
    return this._filterList
  }

  public set filterList(value:string){
    this._filterList = value;
    this.eventosFiltrados = this.filterList ? this.filtrarEventos(this.filterList) : this.eventos
  }

  filtrarEventos(filtrarPor: string):any  {
    filtrarPor = filtrarPor.toLocaleLowerCase(); //passa tudo para minusculo
    return this.eventos.filter(
      (evento : any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 || //ocorreu um erro na video, onde precisou ser colocado (evento : any)
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1   //para pesquisar por local
    )
  }

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.getEventos();
  }

  showImg = true;
  changeImg(){
    this.showImg = !this.showImg; //vai receber o oposto dele (false/true)
  }

  public getEventos(): void { //getEvento é a conexão com o back
    this.http.get('https://localhost:44322/api/Eventos').subscribe(
      response => {
        this.eventos = response
        this.eventosFiltrados = this.eventos;
      },
      error => console.log(error)
      //complete
    );
    //está sendo realizado um get no protocolo http, dentro da url e está se inscrevendo que está retornando 3 itens

  }
}
