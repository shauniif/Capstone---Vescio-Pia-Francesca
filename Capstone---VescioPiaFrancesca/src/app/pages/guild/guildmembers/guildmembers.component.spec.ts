import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuildmembersComponent } from './guildmembers.component';

describe('GuildmembersComponent', () => {
  let component: GuildmembersComponent;
  let fixture: ComponentFixture<GuildmembersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GuildmembersComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GuildmembersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
