import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import {
  Validators,
  FormControl,
  FormGroup,
  FormBuilder,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, filter, takeUntil } from 'rxjs';
import {
  AdminApiPostCategoryApiClient,
  PostCategoryDto,
  UpdatePostCategoryDto,
  AdminApiBlogApiClient,
  CreatePostCategoryDto,
} from 'src/app/api/admin-api.service.generated';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  templateUrl: 'post-category-detail.component.html',
})
export class PostCategoryDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public saveBtnName: string;
  public closeBtnName: string;
  selectedEntity = {} as PostCategoryDto;
  filteredPostCategories: PostCategoryDto[];

  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private postCategoryService: AdminApiPostCategoryApiClient,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private blogService: AdminApiBlogApiClient
  ) {}

  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  public generateSlug() {
    var slug = this.utilService.makeSeoTitle(this.form.get('name').value);
    this.form.controls['slug'].setValue(slug);
  }

  ngOnInit() {
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
      this.saveBtnName = 'Cập nhật';
      this.closeBtnName = 'Hủy';
    } else {
      this.saveBtnName = 'Thêm';
      this.closeBtnName = 'Đóng';
    }
  }

  // Validate
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validationMessages = {
    code: [
      { type: 'required', message: 'Bạn phải nhập mã' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    name: [
      { type: 'required', message: 'Bạn phải nhập tên' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    slug: [{ type: 'required', message: 'Bạn phải nhập mã duy nhất' }],
    sortOrder: [{ type: 'required', message: 'Bạn phải nhập thứ tự' }],
  };

  loadDetail(id: any) {
    this.toggleBlockUI(true);
    this.postCategoryService
      .getPostCategoryById(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PostCategoryDto) => {
          this.selectedEntity = response;
          this.buildForm();
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  saveChange() {
    this.toggleBlockUI(true);

    this.saveData();
  }

  private saveData() {
    console.log(this.form.value);

    if (this.utilService.isEmpty(this.config.data?.id)) {
      var createPostCategoryDto = new CreatePostCategoryDto({
        ...this.form.value,
        parentId: this.form.value.slParent?.id,
      });
      this.postCategoryService
        .createPostCategory(new CreatePostCategoryDto(createPostCategoryDto))
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.ref.close(createPostCategoryDto);
          this.toggleBlockUI(false);
        });
    } else {
      var updatePostCategoryDto = new UpdatePostCategoryDto({
        ...this.form.value,
        id: this.config.data?.id,
        parentId: this.form.value.slParent?.id,
      });
      this.postCategoryService
        .updatePostCategory(updatePostCategoryDto)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(() => {
          this.toggleBlockUI(false);
          this.ref.close(updatePostCategoryDto);
        });
    }
  }

  buildForm() {
    this.form = this.fb.group({
      code: new FormControl(
        this.selectedEntity.code || null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(255),
          Validators.minLength(3),
        ])
      ),
      name: new FormControl(
        this.selectedEntity.name || null,
        Validators.compose([
          Validators.required,
          Validators.maxLength(255),
          Validators.minLength(3),
        ])
      ),
      slug: new FormControl(
        this.selectedEntity.slug || null,
        Validators.required
      ),
      slParent: new FormControl(
        this.selectedEntity.parentId
          ? `${this.selectedEntity.parentCode} - ${this.selectedEntity.parentName}`
          : null
      ),
      sortOrder: new FormControl(
        this.selectedEntity.sortOrder || 0,
        Validators.required
      ),
      seoDescription: new FormControl(
        this.selectedEntity.seoDescription || null
      ),
      isActive: new FormControl(this.selectedEntity.isActive || false),
    });
    if (this.form.get('code').value === null) {
      this.generateRandomCode();
    }
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.btnDisabled = true;
      this.blockedPanelDetail = true;
    } else {
      setTimeout(() => {
        this.btnDisabled = false;
        this.blockedPanelDetail = false;
      }, 1000);
    }
  }

  generateRandomCode() {
    const randomCode = this.utilService.generateRandomCode();
    this.form.controls['code'].setValue(randomCode);
  }

  filterPostCategories(event): void {
    var filter = event.query;
    this.blogService
      .getAllBlogPostCategories(filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((res) => {
        this.filteredPostCategories = res;
      });
  }

  public selectedPostCategoryDisplay(postCategory: PostCategoryDto): string {
    return `${postCategory.code} - ${postCategory.name}`;
  }
}
