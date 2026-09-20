# Screenshot Manager

A safe, local-first .NET 8 CLI for cataloging, searching, exporting, and organizing screenshot/image folders without uploading files anywhere.

**Author:** Radwan Abdulhadi Ahmed · رضوان عبدالهادي أحمد · GitHub: @rad03i2

## English

### Overview
Screenshot Manager helps turn an untidy screenshot folder into a searchable catalog and an optional year-month archive. It is intentionally local and dependency-light: scanning and previewing never modify images, and organization is preview-only unless `--apply` is explicitly supplied.

### Why it exists
Screenshot folders grow quickly and meaningful images become difficult to find. This project provides predictable file-system tools without cloud accounts, telemetry, databases, or destructive defaults.

### Features
- Scan PNG, JPG/JPEG, WebP, BMP and GIF images.
- Optional recursive scanning.
- Case-insensitive filename search.
- Human-readable inventory with timestamp and byte size.
- JSON catalog export for scripts and other tools.
- Organize images into `YYYY-MM` folders using modified time.
- Dry-run/preview by default; `--apply` is required to move files.
- Collision-safe destination naming (`name (1).png`, etc.).
- Per-file I/O errors are reported without hiding partial results.
- Cross-platform .NET 8 implementation; no third-party runtime packages.

### Requirements & installation
Install the .NET 8 SDK, clone the repository, then:

```bash
dotnet build -c Release
dotnet run -- scan "/path/to/screenshots"
```

To create a standalone framework-dependent executable:

```bash
dotnet publish -c Release -o publish
```

### Usage
```bash
# inventory
dotnet run -- scan "Screenshots"

# include nested folders and export metadata
dotnet run -- scan "Screenshots" --recursive --json catalog.json

# filename search
dotnet run -- search "Screenshots" "meeting" --recursive

# preview monthly organization (does not move files)
dotnet run -- organize "Screenshots" "Archive" --recursive

# execute the displayed plan
dotnet run -- organize "Screenshots" "Archive" --recursive --apply
```

### Configuration
There is no config file and no environment variable requirement. All behavior is explicit through CLI arguments.

### Project structure
- `Program.cs` — CLI, validation and exit codes.
- `src/ScreenshotCatalog.cs` — scan, search and JSON export.
- `src/Organizer.cs` — preview planning and safe moves.
- `src/Models.cs` — domain records.
- `tests/ScreenshotManager.Tests/` — automated tests.
- `.github/workflows/ci.yml` — Linux, Windows and macOS CI.

### Testing
```bash
dotnet test tests/ScreenshotManager.Tests/ScreenshotManager.Tests.csproj -c Release
```
Tests cover image filtering, case-insensitive search, recursive behavior, preview semantics and actual organization.

### Preview / screenshots
This is a CLI application, so terminal output is the primary interface. A repository preview image may be added later; no GUI screenshot is claimed by this release.

### Security & privacy
All processing is local. The application has no network code, telemetry, credentials, API keys, or automatic deletion. `organize` moves files only after explicit `--apply`. Back up important data before any bulk file operation.

### Limitations
This release identifies screenshots by common image extensions; it does not inspect image pixels, OCR text, EXIF metadata, or distinguish camera photos from screenshots. Organization uses filesystem modified time rather than EXIF capture time. Moves across filesystems depend on the operating system's `File.Move` behavior.

### Optional roadmap
Optional future work may include content-aware screenshot detection, duplicate hashing, EXIF-aware dates and a desktop UI. These are not implemented features.

### Contributing
Keep changes focused, add tests for behavior changes, run `dotnet test`, and avoid introducing network access or destructive defaults without strong justification.

### License
MIT License — see `LICENSE`.

### Author
**Radwan Abdulhadi Ahmed**  
**رضوان عبدالهادي أحمد**  
GitHub: **@rad03i2**

---

## العربية

### نظرة عامة
Screenshot Manager أداة سطر أوامر مبنية بـ .NET 8 لإدارة مجلدات لقطات الشاشة والصور محليًا. تستطيع فهرسة الصور والبحث في أسمائها وتصدير الفهرس إلى JSON وتنظيم الصور اختياريًا داخل مجلدات شهرية. المعاينة لا تعدل الملفات، ولا يتم النقل إلا عند تمرير `--apply` صراحةً.

### لماذا المشروع؟
تتراكم لقطات الشاشة بسرعة ويصبح العثور على صورة معينة صعبًا. يوفر المشروع أدوات واضحة ومتوقعة بدون حساب سحابي أو تتبع أو قاعدة بيانات أو حذف تلقائي.

### المزايا
- فحص PNG وJPG/JPEG وWebP وBMP وGIF.
- فحص المجلدات الفرعية اختياريًا.
- بحث في أسماء الملفات دون حساسية لحالة الأحرف.
- عرض التاريخ والحجم لكل صورة.
- تصدير الفهرس بصيغة JSON.
- تنظيم الصور داخل مجلدات `YYYY-MM` حسب تاريخ تعديل الملف.
- معاينة افتراضية؛ النقل الفعلي يحتاج `--apply`.
- منع استبدال ملف موجود عبر توليد اسم بديل آمن.
- معالجة أخطاء الملفات بصورة واضحة.
- يعمل على .NET 8 دون حزم تشغيل خارجية.

### المتطلبات والتثبيت
ثبّت .NET 8 SDK ثم استنسخ المستودع وشغّل:

```bash
dotnet build -c Release
dotnet run -- scan "/path/to/screenshots"
```

ولإنشاء نسخة منشورة:

```bash
dotnet publish -c Release -o publish
```

### أمثلة الاستخدام
```bash
# فحص المجلد
dotnet run -- scan "Screenshots"

# فحص فرعي وتصدير JSON
dotnet run -- scan "Screenshots" --recursive --json catalog.json

# البحث
dotnet run -- search "Screenshots" "meeting" --recursive

# معاينة التنظيم فقط
dotnet run -- organize "Screenshots" "Archive" --recursive

# تنفيذ النقل
dotnet run -- organize "Screenshots" "Archive" --recursive --apply
```

### الإعداد
لا يحتاج المشروع إلى ملف إعداد أو متغيرات بيئة أو أسرار. كل الخيارات تمرر مباشرة إلى سطر الأوامر.

### بنية المشروع
`Program.cs` للواجهة، و`src/ScreenshotCatalog.cs` للفهرسة والبحث والتصدير، و`src/Organizer.cs` للتخطيط والنقل، و`tests/` للاختبارات، و`.github/workflows/ci.yml` للتكامل المستمر.

### الاختبارات
```bash
dotnet test tests/ScreenshotManager.Tests/ScreenshotManager.Tests.csproj -c Release
```
تغطي الاختبارات تصفية الصور والبحث والفحص المتكرر والمعاينة والتنظيم الفعلي.

### المعاينة
المشروع حاليًا أداة سطر أوامر، لذلك واجهته الأساسية هي الطرفية. لا يدعي هذا الإصدار وجود واجهة رسومية.

### الأمان والخصوصية
كل العمليات محلية ولا يحتوي المشروع على اتصال شبكي أو Telemetry أو مفاتيح API. لا يوجد حذف تلقائي، ولا يتم نقل الملفات إلا عند استخدام `--apply`. يوصى دائمًا بوجود نسخة احتياطية قبل عمليات الملفات الجماعية.

### القيود
يعتمد التعرف على الصور على امتدادات الملفات ولا يحلل البكسلات أو OCR أو EXIF، ولا يميز تلقائيًا بين صورة كاميرا ولقطة شاشة. يعتمد التنظيم على تاريخ تعديل الملف، وقد تختلف عمليات النقل بين الأقراص حسب نظام التشغيل.

### التطوير المستقبلي الاختياري
يمكن مستقبلًا إضافة اكتشاف معتمد على المحتوى، كشف التكرار بالبصمة، تواريخ EXIF وواجهة سطح مكتب. هذه ليست مزايا موجودة حاليًا.

### المساهمة
يُفضل إبقاء التغييرات محددة، وإضافة اختبار لأي سلوك جديد وتشغيل `dotnet test` قبل الإرسال، مع الحفاظ على مبدأ الخصوصية وعدم استخدام إعدادات مدمرة افتراضيًا.

### الترخيص
MIT — راجع ملف `LICENSE`.

### المؤلف
**Radwan Abdulhadi Ahmed**  
**رضوان عبدالهادي أحمد**  
GitHub: **@rad03i2**
