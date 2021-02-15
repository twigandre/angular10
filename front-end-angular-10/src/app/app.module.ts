import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { FormularioCadastroUsuarioComponent } from './login/formulario-cadastro-usuario/formulario-cadastro-usuario.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { CadernoCinzaComponent } from './caderno-cinza/caderno-cinza.component';
import { DespesasGeraisComponent } from './despesas-gerais/despesas-gerais.component';
import { ComprasComponent } from './compras/compras.component';
import { FaturamentoComponent } from './faturamento/faturamento.component';
import { ItauComponent } from './itau/itau.component';
import { ManterCadernoCinzaComponent } from './caderno-cinza/formulario/manter-caderno-cinza.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    FormularioCadastroUsuarioComponent,
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
    HttpClientModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
