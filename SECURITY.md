# Security Policy / سياسة الأمان

## Supported version
The current `main` branch is supported.

## Security model
Screenshot Manager is local-only and intentionally has no network or telemetry code. Scanning, searching, and JSON export are read-only. Organization is a preview unless `--apply` is explicitly supplied. The tool never deletes source images.

Treat paths and image folders as untrusted input. Files may change between preview and execution; this release does not lock source files or provide transactional rollback. Keep backups for important data and avoid running with elevated privileges unless necessary.

## Reporting
Please report reproducible security problems through GitHub's private vulnerability reporting feature when available rather than publishing sensitive exploit details in a public issue.

## العربية
المشروع يعمل محليًا ولا يرسل الصور أو البيانات إلى الشبكة. الفحص والبحث والتصدير للقراءة فقط، والتنظيم لا ينفذ النقل إلا مع `--apply`. لا يحذف البرنامج الصور. احتفظ بنسخة احتياطية للبيانات المهمة ولا تشغله بصلاحيات مرتفعة دون حاجة. يمكن أن تتغير الملفات بين المعاينة والتنفيذ، ولا يوفر هذا الإصدار قفلًا للملفات أو تراجعًا معاملاتيًا كاملًا.

Maintainer: Radwan Abdulhadi Ahmed / رضوان عبدالهادي أحمد / @rad03i2
