import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { NgxMaskModule, IConfig } from 'ngx-mask'

import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { CadernoCinzaComponent } from './caderno-cinza/caderno-cinza.component';
import { DespesasGeraisComponent } from './despesas-gerais/despesas-gerais.component';
import { ComprasComponent } from './compras/compras.component';
import { FaturamentoComponent } from './faturamento/faturamento.component';
import { ItauComponent } from './itau/itau.component';
import { ManterCadernoCinzaComponent } from './caderno-cinza/formulario/manter-caderno-cinza.component';

const maskConfig: Partial<IConfig> = {
  validation: false,
};

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
    FormsModule,
    NgxMaskModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
