import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItauComponent } from './itau.component';

describe('ItauComponent', () => {
  let component: ItauComponent;
  let fixture: ComponentFixture<ItauComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItauComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItauComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
