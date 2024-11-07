import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExtractosListComponent } from './extractos-list.component';

describe('ExtractosListComponent', () => {
  let component: ExtractosListComponent;
  let fixture: ComponentFixture<ExtractosListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExtractosListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExtractosListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
