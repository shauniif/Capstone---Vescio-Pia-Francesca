import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleNationComponent } from './single-nation.component';

describe('SingleNationComponent', () => {
  let component: SingleNationComponent;
  let fixture: ComponentFixture<SingleNationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SingleNationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SingleNationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
