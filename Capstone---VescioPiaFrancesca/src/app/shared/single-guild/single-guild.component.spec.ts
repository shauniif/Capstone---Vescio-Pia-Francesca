import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SingleGuildComponent } from './single-guild.component';

describe('SingleGuildComponent', () => {
  let component: SingleGuildComponent;
  let fixture: ComponentFixture<SingleGuildComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SingleGuildComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SingleGuildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
