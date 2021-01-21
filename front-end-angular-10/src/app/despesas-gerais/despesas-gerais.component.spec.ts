import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DespesasGeraisComponent } from './despesas-gerais.component';

describe('DespesasGeraisComponent', () => {
  let component: DespesasGeraisComponent;
  let fixture: ComponentFixture<DespesasGeraisComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DespesasGeraisComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DespesasGeraisComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
