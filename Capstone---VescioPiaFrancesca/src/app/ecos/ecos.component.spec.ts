import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcosComponent } from './ecos.component';

describe('EcosComponent', () => {
  let component: EcosComponent;
  let fixture: ComponentFixture<EcosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EcosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EcosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
