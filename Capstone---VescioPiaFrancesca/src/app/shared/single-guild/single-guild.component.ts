import { Component, Input } from '@angular/core';
import { iGuild } from '../../interfaces/i-guild';

@Component({
  selector: 'app-single-guild',
  templateUrl: './single-guild.component.html',
  styleUrl: './single-guild.component.scss'
})
export class SingleGuildComponent {
@Input() guild!: iGuild;
}
