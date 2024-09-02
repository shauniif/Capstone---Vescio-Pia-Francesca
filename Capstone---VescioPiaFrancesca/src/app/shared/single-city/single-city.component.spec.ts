import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleCityComponent } from './single-city.component';

describe('SingleCityComponent', () => {
  let component: SingleCityComponent;
  let fixture: ComponentFixture<SingleCityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SingleCityComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SingleCityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
