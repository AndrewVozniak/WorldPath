import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

@Component({
  selector: 'app-routes-difficult-level',
  templateUrl: './routes-difficult-level.component.html',
  styleUrls: ['./routes-difficult-level.component.scss']
})
export class RoutesDifficultLevelComponent implements OnInit {
  @Input() difficultLevels?: string[]; // Здесь используем вопросительный знак, чтобы указать, что свойство опционально
  @Output() levelsChanged = new EventEmitter<string[]>(); // Output и EventEmitter
  selectedLevels: string[] = [];


  ngOnInit() {
    if (this.difficultLevels) {
      this.selectedLevels = [...this.difficultLevels]; // Создаем копию массива difficultLevels
    }
  }

  toggleLevel(name: string) {
    const index = this.selectedLevels.indexOf(name);

    // Если уровень сложности еще не выбран, то добавляем его в массив
    if (index === -1) {
      this.selectedLevels.push(name);
    }
    // Если уровень сложности уже выбран, то удаляем его из массива
    else {
      this.selectedLevels.splice(index, 1);
    }

    this.levelsChanged.emit(this.selectedLevels);
  }
}
