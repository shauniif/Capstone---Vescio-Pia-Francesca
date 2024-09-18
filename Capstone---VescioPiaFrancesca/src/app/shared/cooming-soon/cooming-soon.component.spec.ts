import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoomingSoonComponent } from './cooming-soon.component';

describe('CoomingSoonComponent', () => {
  let component: CoomingSoonComponent;
  let fixture: ComponentFixture<CoomingSoonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CoomingSoonComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoomingSoonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
