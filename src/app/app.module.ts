import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { CadernoCinzaComponent } from './caderno-cinza/caderno-cinza.component';
import { DespesasGeraisComponent } from './despesas-gerais/despesas-gerais.component';
import { ComprasComponent } from './compras/compras.component';
import { FaturamentoComponent } from './faturamento/faturamento.component';
import { ItauComponent } from './itau/itau.component';
import { ManterCadernoCinzaComponent } from './caderno-cinza/formulario/manter-caderno-cinza.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    CadernoCinzaComponent,
    DespesasGeraisComponent,
    ComprasComponent,
    FaturamentoComponent,
    ItauComponent,
    ManterCadernoCinzaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
