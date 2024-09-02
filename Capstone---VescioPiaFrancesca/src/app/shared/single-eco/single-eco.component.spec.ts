import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleEcoComponent } from './single-eco.component';

describe('SingleEcoComponent', () => {
  let component: SingleEcoComponent;
  let fixture: ComponentFixture<SingleEcoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SingleEcoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SingleEcoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
