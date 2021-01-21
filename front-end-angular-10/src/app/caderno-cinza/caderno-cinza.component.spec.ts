import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CadernoCinzaComponent } from './caderno-cinza.component';

describe('CadernoCinzaComponent', () => {
  let component: CadernoCinzaComponent;
  let fixture: ComponentFixture<CadernoCinzaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CadernoCinzaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CadernoCinzaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
